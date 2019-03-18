using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class WinScoreScript : MonoBehaviour {

	private int actualScore = 0;

	void Start () {
		if (this.actualScore > 0) {
			this.gameObject.GetComponent<Text>().text = "Score: " + this.actualScore.ToString();
		} else {
			this.gameObject.GetComponent<Text> ().text = "";
		}
	}

	public void setScore(int score) {
		this.actualScore = score;
		this.gameObject.GetComponent<Text>().text = "Score: " + score.ToString();
	}
}
