using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class LevelDisplayScript : MonoBehaviour {

	public GameObject HUDCanvas;
	public GameObject levelCanvas;

	// Managers
	public GameObject board;
	public GameObject pilot;

	public void showLevel(int level) {
		Text text = this.gameObject.GetComponent<Text> ();

		if (level == 5) {
			text.text = "Bonus Level\nNo Repositioning";
		} else {
			text.text = String.Format ("Level {0}", level);
		}

		StartCoroutine (waitToSpawn(level));
	}

	IEnumerator waitToSpawn(int level) {
		yield return new WaitForSeconds(3);
		startSpawning (level);
	}

	public void startSpawning(int level) {
		// tear down instructions
		levelCanvas.SetActive(false);

		HUDCanvas.SetActive(true);
		pilot.SetActive (true);
		pilot.GetComponent<PilotScript> ().setLevel (level);
		board.GetComponent<SpawnScript> ().startSpawning (level);
	}

}
