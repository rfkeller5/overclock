using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnemyType {
	BASIC = 0,
	FAST,
	STRONG,
	FAST_STRONG,
	ICE_RESISTANT
}

public class BasicEnemyScript : MonoBehaviour {

	private Color whiteColor = new Color (255, 255, 255);
	private Color blueColor = new Color (0, 110, 255);
	private Color redColor = new Color (255, 0, 0);
	private Color yellowColor = new Color (255, 255, 0);

	private const float slownessDuration = 3.0f;
	private const float defaultAttackSpeed = 1.0f;

	public float normalVelocity;
	public float slowVelocity;
	public int health = 15;
	private int startingHealth = 15;
	public int attackPower = 1;
	public float initialAttackSpeed = defaultAttackSpeed;
	public EnemyType enemyType;
	public bool dropCurrency = true;

	private float currentAttackSpeed = defaultAttackSpeed;
	private float attackTimer;
	private float slowTimer = 0.0f;
	public bool isDying = false;

	private Animator anim;

	private const int WALK = 0;
	private const int ATTACK = 1;
	private const int DEAD = 2;

	private List<GameObject> touchingHeroes = new List<GameObject>();

	public GameObject healthBar;
	private GameObject activeHealthBar;
	public GameObject board;
	public GameObject scrapPiece;
	public AudioSource hitSound;
	public AudioSource pierceSound;
	public AudioSource attackSound;
	public AudioSource dieSound;

	protected bool paused = false;

	// Life-cycle

	virtual protected void Start () {
		anim = GetComponent<Animator> ();

		moveForward ();
		attackTimer = defaultAttackSpeed;
		startingHealth = health;

		activeHealthBar = (GameObject)Instantiate (healthBar, new Vector2 (transform.position.x, transform.position.y + 0.75f), Quaternion.identity);
	}

	void OnPauseGame() {
		paused = true;
	}

	void OnResumeGame() {
		paused = false;
	}

	virtual protected void Update() {
		if (paused) {
			return;
		}
		if (this.isDying) {
			return;
		}

		activeHealthBar.transform.position = new Vector2 (transform.position.x, transform.position.y + 0.75f);

		if (transform.position.x < -4.8f) {
			board.GetComponent<SpawnScript> ().endGame (false);
		}

		if (slowTimer <= 0.0f) {
			if (GetComponent<Rigidbody2D> ().velocity.x != normalVelocity) {
				currentAttackSpeed = initialAttackSpeed;
				moveForward ();
			} 
		} else {
			slowTimer -= Time.deltaTime;
		}

		touchingHeroes.RemoveAll (hero => hero == null);
		moveForward ();

		float healthPercentage = (float)health / (float)startingHealth;
		activeHealthBar.transform.localScale = new Vector3 (healthPercentage, 1.0f, 1.0f);
		if (healthPercentage <= 0.5f) {
			activeHealthBar.GetComponent<SpriteRenderer> ().color = redColor;
		} else if (healthPercentage <= 0.75f) {
			activeHealthBar.GetComponent<SpriteRenderer> ().color = yellowColor;
		}
	}

	// collisions
	
	void OnTriggerEnter2D(Collider2D other) {
		if (colliderIsBullet(other)) {
			BulletScript bulletScript = other.gameObject.GetComponent<BulletScript> ();

			int damageToTake = bulletScript.damage;

			switch (bulletScript.bulletType) {
			case BulletType.BASIC:
				if (enemyType == EnemyType.FAST_STRONG || enemyType == EnemyType.STRONG) {
					damageToTake /= 2;
				}
				break;
			case BulletType.ICE:
				if (enemyType != EnemyType.ICE_RESISTANT) {
					freeze ();
				}
				if (enemyType == EnemyType.FAST_STRONG || enemyType == EnemyType.STRONG) {
					damageToTake /= 2;
				}
				break;
			case BulletType.HEAVY_ICE:
				if (enemyType != EnemyType.ICE_RESISTANT) {
					freeze ();
				}
				break;
			default:
				break;
			}

			switch (this.enemyType) {
			case EnemyType.BASIC:
			case EnemyType.STRONG:
			case EnemyType.ICE_RESISTANT:
				pierceSound.Play ();
				break;
			case EnemyType.FAST:
			case EnemyType.FAST_STRONG:
				hitSound.Play ();
				break;
			default:
				break;
			}

			takeDamage (damageToTake);

		} else if (colliderIsHero (other)) {
			if (paused) {
				return;
			}
			MovableHeroScript moveableHero = other.GetComponent<MovableHeroScript> ();
			if (moveableHero != null && moveableHero.isDragging) {
				return;
			} else {
				isTouchingHero (other);
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (paused) {
			return;
		}
		if (colliderIsHero(other) && this.isDying == false) {

			MovableHeroScript moveableHero = other.GetComponent<MovableHeroScript> ();
			if (moveableHero != null && moveableHero.isDragging) {
				return;
			} else {
				isTouchingHero (other);
			}

			HeroScript heroCharacter = other.GetComponent<HeroScript> ();

			attackTimer -= Time.deltaTime / currentAttackSpeed;

			if (attackTimer <= 0.0f && heroIsInFront(other)) {
				attackTimer = defaultAttackSpeed;
				attackSound.Play ();
				bool didKillhero = heroCharacter.takeDamage (attackPower);
				if (didKillhero) {
					stoppedTouchingHero (other);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (paused) {
			return;
		}
		if (colliderIsHero (other)) {
			stoppedTouchingHero (other);
		}
	}

	public bool takeDamage(int dmg) {
		health -= dmg;

		if (health <= 0 && this.isDying == false) {
			if (dropCurrency) {
				Instantiate (scrapPiece, new Vector2 (this.transform.position.x, this.transform.position.y), Quaternion.identity);
			}

			activeHealthBar.SetActive (false);

			if (anim) {
				anim.SetInteger ("State", DEAD);
				dieSound.Play ();
			}
			stopMoving ();
			this.isDying = true;
			StartCoroutine (waitToDie ());
		}

		return health <= 0;
	}

	IEnumerator waitToDie() {
		yield return new WaitForSeconds(1);

		Destroy (activeHealthBar);
		Destroy (gameObject);
	}

	 // movement helpers

	virtual protected void moveForward() {
		if (touchingHeroes.Count == 0 && health > 0) {
			if (anim) {
				anim.SetInteger ("State", WALK);
			}
			if (slowTimer > 0.0f) {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (slowVelocity, 0.0f);
				gameObject.GetComponent<SpriteRenderer> ().color = blueColor;
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (normalVelocity, 0.0f);
				gameObject.GetComponent<SpriteRenderer> ().color = whiteColor;
			}
		}
	}

	virtual protected void stopMoving() {
		GetComponent<Rigidbody2D>().velocity = new Vector2 (0.0f, 0.0f);
	}

	virtual protected void freeze() {
		currentAttackSpeed = initialAttackSpeed * 2;
		slowTimer = slownessDuration;
		moveForward ();
	}

	// collision helpers

	private bool colliderIsBullet(Collider2D other) {
		return other.gameObject.tag == "Bullet";
	}

	private bool colliderIsHero(Collider2D other) {
		return other.gameObject.tag == "Hero";
	}

	private bool heroIsInFront(Collider2D hero) {
		float xPosition = hero.transform.position.x;

		foreach (var h in touchingHeroes) {
			if (h.transform.position.x > xPosition) {
				return false;
			}
		}

		return true;
	}

	virtual protected void isTouchingHero(Collider2D hero) {
		if (touchingHeroes.Contains (hero.gameObject) == false) {
			touchingHeroes.Add (hero.gameObject);
		}

		if (anim) {
			anim.SetInteger ("State", ATTACK);
		}
		stopMoving ();
	}

	virtual protected void stoppedTouchingHero(Collider2D hero) {
		touchingHeroes.Remove (hero.gameObject);
		attackTimer = defaultAttackSpeed;
		moveForward ();
	}
}
