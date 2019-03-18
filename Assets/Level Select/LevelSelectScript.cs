using UnityEngine;
using System.Collections;

public class LevelSelectScript : MonoBehaviour {

	public int selectedLevel = 1;
	public const int max_level = 8;

	void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}
}
