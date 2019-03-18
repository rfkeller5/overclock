using UnityEngine;
using System.Collections;

public class LetsGoScript : MonoBehaviour {

	// Canvases
	public GameObject levelCanvas;
	public GameObject instructionsCanvas;
	public GameObject tutorialCanvas;

	public GameObject levelText;


	private int level = 1;

	void Start() {
		GameObject levelSelectObject = GameObject.Find ("PersistingObject");

		if (levelSelectObject == null) {
			this.level = 1;
		} else {
			this.level = levelSelectObject.GetComponent<LevelSelectScript> ().selectedLevel;
		}

		if (this.level != 1) {
			showLevel ();
		}
	}

	public void showLevel() {
		// tear down instructions
		instructionsCanvas.SetActive(false);

		if (level == 1) {
			showTutorial ();
		} else {
			levelCanvas.SetActive (true);
			levelText.GetComponent<LevelDisplayScript> ().showLevel (this.level);
		}
	}

	void showTutorial() {
		tutorialCanvas.SetActive (true);
	}

}
