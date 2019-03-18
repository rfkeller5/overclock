using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunnerScript : MovableHeroScript {

	public GameObject simpleBullet;
	public GameObject heavyBullet;
	public GameObject iceBullet;
	public GameObject heavyIceBullet;
	public AudioSource lazerShot;
	public AudioSource normalShot;
	public AudioSource thudShot;

	const float fireRatePerSecond = 1.0f;
	float fireTimer = fireRatePerSecond;

	private Animator anim;

	private const int IDLE = 0;
	private const int SHOOT = 1;
	private const int DEAD = 2;
	private const int DRAGGING = 3;
	private const int IDLE_AIM = 4;

	private bool isWaitingToIdle = false;

	override protected void Start() {
		base.Start ();

		heroType = HeroType.GUNNER;

		anim = GetComponent<Animator> ();
	}

	override protected bool willRemoveFromBoard() {
		anim.SetInteger ("State", DEAD);

		StartCoroutine (waitToDie ());

		base.willRemoveFromBoard ();

		return false;
	}

	GameObject fireSimpleBullet () {
		normalShot.Play ();
		return (GameObject)Instantiate (simpleBullet, new Vector3 (transform.position.x + 0.65f, transform.position.y, 0.0f), Quaternion.identity);
	}

	GameObject fireIceBullet () {
		lazerShot.Play ();
		return (GameObject)Instantiate (iceBullet, new Vector3 (transform.position.x + 0.65f, transform.position.y, 0.0f), Quaternion.identity);
	}

	GameObject fireSniperBullet () {
		thudShot.Play ();
		return (GameObject)Instantiate (heavyBullet, new Vector3 (transform.position.x + 0.65f, transform.position.y, 0.0f), Quaternion.identity);
	}

	GameObject fireHeavyIceBullet() {
		thudShot.Play ();
		return (GameObject)Instantiate (heavyIceBullet, new Vector3 (transform.position.x + 0.65f, transform.position.y, 0.0f), Quaternion.identity);
	}

	override protected void Update() {
		if (paused) {
			return;
		}
		base.Update ();

		if (isDragging || isOnBench) {
			if (isDragging) {
				anim.SetInteger ("State", DRAGGING);
			} else if (isOnBench) {
				idle (false);
			}

			fireTimer = fireRatePerSecond;
			return;
		}

		bool enemyAhead = isEnemyAhead ();

		idle (enemyAhead);

		if (enemyAhead) {
			List<HeroType> powerList = PowerManagement.getPowerList ((uint)colPos, (uint)rowPos);

			fireTimer -= Time.deltaTime;

			bool quickShot = false;
			if (powerList.Contains (HeroType.SPEED_POWER_CONVERTER)) {
				fireTimer -= Time.deltaTime; // subtract it again to double the speed
				quickShot = true;
			}

			if (fireTimer <= 0.0f) {
				fireTimer = fireRatePerSecond;
				anim.SetInteger ("State", SHOOT);
				StartCoroutine (waitToIdle ());

				GameObject bullet;

				// The problem here is that it can be both heavy and ice
				// need to find a way to make this scale better
				if (powerList.Contains (HeroType.DAMAGE_POWER_CONVERTER)) {
					if (powerList.Contains (HeroType.ICE_POWER_CONVERTER)) {
						bullet = fireHeavyIceBullet ();
					} else {
						bullet = fireSniperBullet ();
					}
				} else if (powerList.Contains (HeroType.ICE_POWER_CONVERTER)) {
					bullet = fireIceBullet ();
				} else {
					bullet = fireSimpleBullet ();
				}

				bullet.GetComponent<BulletScript> ().setWasQuickShot (quickShot);
			}

			return;
		} 
	}

	private bool isEnemyAhead() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject enemy in enemies) {
			if (enemy.tag == "Enemy" && 
				enemy.transform.position.y <= this.transform.position.y + 0.3f &&
				enemy.transform.position.y >= this.transform.position.y - 0.3f &&
			    enemy.transform.position.x > this.transform.position.x && enemy.transform.position.x < 9.0f) {
				return true;
			}
		}
		return false;
	}


	// ANIMATION HELP

	void idle(bool enemyAhead) {
		if (!isWaitingToIdle && !isDragging) {
			if (enemyAhead) {
				anim.SetInteger ("State", IDLE_AIM);
			} else {
				anim.SetInteger ("State", IDLE);
			}
		}
	}

	IEnumerator waitToIdle() {
		isWaitingToIdle = true;
		yield return new WaitForSeconds(0.2f);
		isWaitingToIdle = false;
		idle (isEnemyAhead());
	}

	IEnumerator waitToDie() {
		yield return new WaitForSeconds(1);
		die ();
	}

}
