using UnityEngine;
using System.Collections;

public class ScrapPieceScript : MonoBehaviour {

	public int scrapValue = 40;

	private Vector2 destination = new Vector2(-8.0f, 2.5f);

	public float lerpTime;
	private float currentLerpTime = 0.0f;

	public float floatStrength;
	public float floatSpeed;
	private float yOrigin;

	public float autoTapTimout;
	private float autoTapTimer = 0.0f;
	private bool wasTapped = false;

	protected bool paused = false;

	void Start () {
		yOrigin = transform.position.y;
	}

	void OnPauseGame() {
		paused = true;
	}

	void OnResumeGame() {
		paused = false;
	}

	void Update () {
		if (paused) {
			return;
		}
		if (wasTapped) {
			currentLerpTime += Time.deltaTime;
			if (currentLerpTime > lerpTime) {
				currentLerpTime = lerpTime;
			}

			float percentage = currentLerpTime / lerpTime;
			transform.position = Vector3.Lerp (transform.position, destination, percentage);

			float distanceLeft = Vector2.Distance (transform.position, destination);
			if (distanceLeft <= 0.2f) {
				GameObject scrapPile = GameObject.FindGameObjectWithTag ("Scrap");
				ScrapManagerScript scrap = scrapPile.GetComponent<ScrapManagerScript> ();
				scrap.addScrap (scrapValue);

				Destroy (this.gameObject);
			}
		} else {
			// in the meantime, hover
			transform.position = new Vector2(transform.position.x, yOrigin + (Mathf.Sin(Time.time * floatSpeed) * floatStrength));

			autoTapTimer += Time.deltaTime;
			if (autoTapTimer >= autoTapTimout) {
				wasTapped = true;
			}
		}
	}

	void OnMouseUp() {
		wasTapped = true;
	}
}
