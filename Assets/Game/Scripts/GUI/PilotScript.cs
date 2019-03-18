using UnityEngine;
using System.Collections;

public class PilotScript : MonoBehaviour {

	public GameObject board;

	private float[] runSpeeds = new float[] { -0.14f, -0.1f, -0.07f, -0.07f, -0.07f, -0.05f, -0.05f, -0.05f };

	public void setLevel(int level) {
		int index = 0;
		if (level - 1 < runSpeeds.Length && level - 1 >= 0) {
			index = level - 1;
		}
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (runSpeeds[index], 0.0f);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "CheckeredFlag") {
			board.GetComponent<SpawnScript> ().endGame (true);
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0.0f, 0.0f);
		}
	}
}
