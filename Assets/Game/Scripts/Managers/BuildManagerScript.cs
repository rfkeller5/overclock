using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildManagerScript : MonoBehaviour {

	public GameObject scrapPile;

	public static Vector2 offScreen = new Vector2 (-50.0f, -50.0f);
	public const float xPosition = -6.37f;

	protected List<GameObject> benchQueue = new List<GameObject>();

	private ScrapManagerScript scrapManager;

	void Start() {
		scrapManager = scrapPile.GetComponent<ScrapManagerScript> ();
	}

	public bool buildObject(GameObject go, int cost, bool delayedImmovable) {
		if (benchQueue.Count < 5 && scrapManager.useScrap(cost)) {
			benchQueue.Add (build(go, delayedImmovable));
			updatePositions ();
			return true;
		}
		return false;
	}

	public void deconstuctObject(int value) {
		scrapManager.addScrap (value);
	}

	public bool removeFromBench(GameObject go) {
		bool didRemove = benchQueue.Remove (go);
		updatePositions ();
		return didRemove;
	}

	public void updatePositions() {
		for (int i = 0; i < benchQueue.Count; ++i) {
			benchQueue [i].transform.position = new Vector2 (xPosition, StageScript.startingRow - (StageScript.tileDimension * i));
		}
	}

	private GameObject build(GameObject go, bool delayedImmovable) {
		GameObject builtObject = (GameObject)Instantiate (go, offScreen, Quaternion.identity);
		builtObject.GetComponent<MovableHeroScript> ().buildBench = this.gameObject;
		builtObject.GetComponent<MovableHeroScript> ().delayedImmovable = delayedImmovable;
		return builtObject;
	}
}
