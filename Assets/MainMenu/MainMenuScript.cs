using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public void goToGame() {
		Debug.Log("Start Game clicked");
		SceneManager.LoadScene("LevelSelect");
	}

	public void goToCredits() {
		Debug.Log("Credits clicked");
		SceneManager.LoadScene("Credits");
	}
}
