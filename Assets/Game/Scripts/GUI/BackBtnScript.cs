using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BackBtnScript : MonoBehaviour {

	public GameObject infoCanvas;

	public void goToMainMenu() {
		SceneManager.LoadScene("LevelSelect");
	}

}
