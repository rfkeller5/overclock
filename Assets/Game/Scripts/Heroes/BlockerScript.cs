using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockerScript : MovableHeroScript {

	public int attackPower = 1;
	public float attackSpeed = 1.5f;
	public float attackAnimSpeed = 1.0f;
	public AudioSource attackSound;

	private float attackTimer;
	private float attackAnimTimer;

	private Animator anim;

	private const int IDLE = 0;
	private const int MELEE = 1;
	private const int RUN = 2;
	private const int DEAD = 3;

	private List<GameObject> touchingEnemies = new List<GameObject>();

	private bool waitingToDie = false;

	override protected void Start () {
		base.Start ();

		heroType = HeroType.BLOCKER;

		anim = GetComponent<Animator> ();
	}

	override protected void Update() {
		if (paused) {
			return;
		}
		base.Update ();

		touchingEnemies.RemoveAll (enemy => enemy == null);

		if (attackTimer > 0.0f) {
			attackTimer -= Time.deltaTime;
		}

		if (attackAnimTimer > 0.0f) {
			attackAnimTimer -= Time.deltaTime;
		}

		if (!waitingToDie && attackAnimTimer <= 0.0f) {
			if (isDragging) {
				anim.SetInteger ("State", RUN);
			} else {
				anim.SetInteger ("State", IDLE);
			}
		}
	}

	override protected bool willRemoveFromBoard() {
		anim.SetInteger ("State", DEAD);
		StartCoroutine (waitToDie ());

		base.willRemoveFromBoard ();

		return false;
	}

	IEnumerator waitToDie() {
		waitingToDie = true;
		yield return new WaitForSeconds(1);
		die ();
	}

	// collisions

	void OnTriggerEnter2D(Collider2D other) {
		if (paused) {
			return;
		}
		if (colliderIsEnemy (other)) {
			BasicEnemyScript enemy = other.GetComponent<BasicEnemyScript> ();
			if (enemy != null && enemy.isDying == false && this.isDying == false) {
				isTouchingEnemy (other);
			}
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (paused) {
			return;
		}
		if (colliderIsEnemy(other) && this.isDying == false) {

			BasicEnemyScript enemy = other.GetComponent<BasicEnemyScript> ();
			if (enemy == null || enemy.isDying) {
				return;
			} else {
				isTouchingEnemy (other);
			}

			if (attackTimer <= 0.0f) {
				attackTimer = attackSpeed;
				attackAnimTimer = attackAnimSpeed;
				anim.SetInteger ("State", MELEE);
				attackSound.Play ();
				bool didKillEnemy = enemy.takeDamage (attackPower);
				if (didKillEnemy) {
					stoppedTouchingEnemy (other);
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (paused) {
			return;
		}
		if (colliderIsEnemy (other)) {
			stoppedTouchingEnemy (other);
		}
	}

	private bool colliderIsEnemy(Collider2D other) {
		return other.gameObject.tag == "Enemy";
	}

	virtual protected void isTouchingEnemy(Collider2D enemy) {
		if (touchingEnemies.Contains (enemy.gameObject) == false) {
			touchingEnemies.Add (enemy.gameObject);
		}
	}

	virtual protected void stoppedTouchingEnemy(Collider2D enemy) {
		touchingEnemies.Remove (enemy.gameObject);
	}

}
