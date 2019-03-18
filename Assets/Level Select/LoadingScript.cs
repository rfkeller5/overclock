using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScript : MonoBehaviour {

	private Text text;

	private float updateSpeed = 0.4f;
	private float currentTime = 0.0f;

	public bool loadingGame = false;

	// Use this for initialization
	void Start () {
		GameObject[] levelItems = GameObject.FindGameObjectsWithTag ("LevelItem");

		foreach (GameObject levelItem in levelItems) {
			levelItem.SetActive (false);
		}

		text = this.gameObject.GetComponent<Text> ();
		text.text = "Loading.";

		currentTime = 0.0f;
		StartCoroutine (LoadNewScene());
	}
	
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;

		if (currentTime >= updateSpeed) {
			currentTime = 0.0f;

			switch (text.text.Length) {
			case 7:
				text.text = "Loading.";
				return;
			case 8:
				text.text = "Loading..";
				return;
			case 9:
				text.text = "Loading...";
				return;
			case 10:
				text.text = "Loading";
				return;
			default:
				return;
			}
		}
	}

	IEnumerator LoadNewScene() {
		yield return new WaitForSeconds(1);
		AsyncOperation async;
		if (loadingGame) {
			async = SceneManager.LoadSceneAsync ("Game");
		} else {
			async = SceneManager.LoadSceneAsync ("MainMenu");
		}
		while (!async.isDone) {
			yield return null;
		}
	}
}
