using UnityEngine;
using System.Collections;

public class InfoScript : MonoBehaviour {

	public GameObject infoCanvas;

	private bool showingInfo = false;

	public void toggleInfo() {
		showingInfo = !showingInfo;
		updateInfoCanvas ();
	}

	public void hideInfo() {
		showingInfo = false;
		updateInfoCanvas ();
	}

	private void updateInfoCanvas() {
		infoCanvas.SetActive (showingInfo);

		Object[] objects = FindObjectsOfType (typeof(GameObject));
		if (showingInfo) {
			Time.timeScale = 0;
			
			foreach (GameObject go in objects) {
				go.SendMessage ("OnPauseGame", SendMessageOptions.DontRequireReceiver);
			}
		} else {
			Time.timeScale = 1;

			foreach (GameObject go in objects) {
				go.SendMessage ("OnResumeGame", SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
