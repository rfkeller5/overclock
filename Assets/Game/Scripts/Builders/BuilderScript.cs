using UnityEngine;
using System.Collections;

public class BuilderScript : MonoBehaviour {

	public GameObject buildBench;
	public GameObject buildableObject;
	public int cost;
	public bool excludedFromBonus = false;

	protected GameObject levelSelectObject;
	protected bool paused = false;

	void Start() {
		levelSelectObject = GameObject.Find ("PersistingObject");

		if (isBonusLevel() && excludedFromBonus) {
			this.gameObject.SetActive (false);
		}
	}

	public void buildObject() {
		if (!paused) {
			buildBench.GetComponent<BuildManagerScript> ().buildObject (buildableObject, cost, isBonusLevel());
		}
	}

	void OnPauseGame() {
		paused = true;
	}

	void OnResumeGame() {
		paused = false;
	}

	protected bool isBonusLevel() {
		return levelSelectObject != null && levelSelectObject.GetComponent<LevelSelectScript> ().selectedLevel == 5;
	}

}
