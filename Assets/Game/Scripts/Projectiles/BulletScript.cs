using UnityEngine;
using System.Collections;

public enum BulletType {
	BASIC = 0,
	ICE,
	HEAVY,
	HEAVY_ICE,
	FIREBALL
}

public class BulletScript : MonoBehaviour {

	public int damage = 1;
	public float bulletVelocity = 15.0f;
	public BulletType bulletType;

	public GameObject onePoint;
	public GameObject twoPoint;
	public GameObject threePoint;
	public GameObject fourPoint;

	private bool wasQuickShot = false;
	private GameObject scoreText;

	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2 (bulletVelocity, 0.0f);
		scoreText = GameObject.FindGameObjectWithTag ("Score");
	}

	public void setWasQuickShot(bool wqs) {
		wasQuickShot = wqs;
	}

	void FixedUpdate() {
		if (transform.position.x > 20.0f) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Enemy" && bulletType != BulletType.FIREBALL) {
			if (scoreText != null) {
				int pointsToAdd = 100;

				switch (bulletType) {
				case BulletType.ICE:
				case BulletType.HEAVY:
					pointsToAdd += 100;
					break;
				case BulletType.HEAVY_ICE:
					pointsToAdd += 200;
					break;
				default:
					break;
				}

				if (wasQuickShot) {
					pointsToAdd += 100;
				}

//				switch (pointsToAdd) {
//				case 100:
//					Instantiate (onePoint, new Vector3 (transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
//					break;
//				case 200:
//					Instantiate (twoPoint, new Vector3 (transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
//					break;
//				case 300:
//					Instantiate (threePoint, new Vector3 (transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
//					break;
//				case 400:
//					Instantiate (fourPoint, new Vector3 (transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
//					break;
//				default:
//					Instantiate (onePoint, new Vector3 (transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
//					break;
//				}

				scoreText.GetComponent<ScoreScript> ().addPoints (pointsToAdd);
			}
			Destroy (gameObject);
		}
	}
}
