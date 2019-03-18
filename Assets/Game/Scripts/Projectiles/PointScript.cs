using UnityEngine;
using System.Collections;

public class PointScript : MonoBehaviour {

	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector2 (-0.4f, 0.4f);
		waitToDie ();

		StartCoroutine (waitToDie());
	}

	IEnumerator waitToDie() {
		yield return new WaitForSeconds(1.5f);
		Destroy (this.gameObject);
	}
}
