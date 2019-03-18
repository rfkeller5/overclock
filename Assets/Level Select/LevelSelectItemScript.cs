using UnityEngine;
using System.Collections;

public class LevelSelectItemScript : MonoBehaviour {

	public int levelNumber = 0;
	public GameObject loading;

	public void goToGame() {
		loading.GetComponent<LoadingScript> ().loadingGame = true;
		loading.SetActive (true);

		GameObject.Find ("PersistingObject").GetComponent<LevelSelectScript> ().selectedLevel = levelNumber;
	}
}
