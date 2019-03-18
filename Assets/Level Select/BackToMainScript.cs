using UnityEngine;
using System.Collections;

public class BackToMainScript : MonoBehaviour {

	public GameObject loading;

	public void backToMainMenu() {
		loading.GetComponent<LoadingScript> ().loadingGame = false;
		loading.SetActive (true);
	}
}
