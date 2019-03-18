using UnityEngine;
using System.Collections;

public class FinalWaveScript : MonoBehaviour {

	void Start () {
		StartCoroutine (waitToDismiss ());
	}
	
	IEnumerator waitToDismiss() {
		yield return new WaitForSeconds(3);
		this.gameObject.SetActive (false);
	}
}
