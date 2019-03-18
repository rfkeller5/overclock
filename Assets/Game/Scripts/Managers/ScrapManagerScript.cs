using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScrapManagerScript : MonoBehaviour {

	public int scrap;
	private int actualScrap;
	private int countingSpeed = 3;

	protected bool paused = false;

	public GameObject textObject;

	void Start () {
		actualScrap = scrap;
		updateText ();
	}

	void OnPauseGame() {
		paused = true;
	}

	void OnResumeGame() {
		paused = false;
	}

	void FixedUpdate () {
		if (paused) {
			return;
		}
		int difference = actualScrap - scrap;
		if (difference >= countingSpeed) {
			scrap = scrap + countingSpeed;
		} else if (difference <= -countingSpeed) {
			scrap = scrap - countingSpeed;
		} else {
			scrap = actualScrap;
		}

		updateText ();
	}
	
	public void addScrap(int amount) {
		this.actualScrap += amount;
	}

	public bool useScrap(int amount) {
		if (this.actualScrap < amount) {
			return false;
		} else {
			this.actualScrap -= amount;
			return true;
		}
	}

	public void updateText() {
		textObject.GetComponent<Text>().text = scrap.ToString();
	}
}
