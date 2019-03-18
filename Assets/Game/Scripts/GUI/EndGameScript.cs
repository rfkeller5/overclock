using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndGameScript : MonoBehaviour {

	LevelSelectScript levelSelectScript;

	public Text nextLevelText;
	public bool isWinCanvas = false;

	void Start() {
		levelSelectScript = GameObject.Find ("PersistingObject").GetComponent<LevelSelectScript>();

		if (isWinCanvas && isLastLevel()) {
			nextLevelText.text = "Credits";
		}
	}

	public void retry() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void goToNextLevel() {
		if (levelSelectScript == null) {
			goToLevelSelect ();
		} else if (isLastLevel()) {
			goToCredits ();
		} else {
			levelSelectScript.selectedLevel += 1;
			retry ();
		}
	}

	public void goToLevelSelect() {
		SceneManager.LoadScene("LevelSelect");
	}

	public void goToCredits() {
		SceneManager.LoadScene("Credits");
	}

	// helper
	protected bool isLastLevel() {
		return levelSelectScript && levelSelectScript.selectedLevel == LevelSelectScript.max_level;
	}
}
