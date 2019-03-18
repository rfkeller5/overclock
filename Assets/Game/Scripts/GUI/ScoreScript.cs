using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreScript : MonoBehaviour {

	private int score = 0;
	private int actualScore = 0;
	private const int countingSpeed = 30;

	protected bool paused = false;

	void Start () {
		score = 0;
		actualScore = 0;
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
		int difference = actualScore - score;
		if (difference >= countingSpeed) {
			score = score + countingSpeed;
		} else if (difference <= -countingSpeed) {
			score = score - countingSpeed;
		} else {
			score = actualScore;
		}

		updateText ();
	}

	public void addPoints (int points) {
		actualScore += points;
	}

	public int getScore() {
		return actualScore;
	}

	public void updateText() {
		this.gameObject.GetComponent<Text>().text = "Score: " + score.ToString();
	}
}
