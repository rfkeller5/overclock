using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CreditsScript : MonoBehaviour {

	public void goToMainMenu() {
		SceneManager.LoadScene("MainMenu");
	}
}
