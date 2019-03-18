using UnityEngine;
using System.Collections;

public class BuyMoreHereScript : MonoBehaviour {

	void Start () {
		GameObject levelSelectObject = GameObject.Find ("PersistingObject");

		if (!levelSelectObject || levelSelectObject.GetComponent<LevelSelectScript> ().selectedLevel != 1) {
			this.gameObject.SetActive (false);
		} else {
			StartCoroutine (waitToHide());
		}
	}

	IEnumerator waitToHide() {
		yield return new WaitForSeconds(3);
		this.gameObject.SetActive (false);
	}

}
