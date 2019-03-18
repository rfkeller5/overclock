using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	// enemy
	public GameObject basicEnemy;
	public GameObject fastEnemy;
	public GameObject toughEnemy;
	public GameObject fastToughEnemy;
	public GameObject iceResistantEnemy;

	// heroes
	public GameObject powerConverter;
	public GameObject iceConverter;
	public GameObject speedConverter;

	// others
	public GameObject buildBench;
	public GameObject hudCanvas;
	public GameObject tutorialCanvas;
	public GameObject winCanvas;
	public GameObject winScore;
	public GameObject loseCanvas;
	public GameObject finalWaveCanvas;

	// state
	private bool lastWaveInitialized = false;
	private bool didFinishGame = false;
	private int currentLevel = 1;

	public void startSpawning(int level) {
		this.currentLevel = level;

		switch (level) {
		case 1:
			InvokeRepeating ("createBasicEnemy", 4, 10);
			InvokeRepeating ("createBasicEnemy", 26, 10);
			InvokeRepeating ("createBasicEnemy", 40, 8);
			break;
		case 2:
			InvokeRepeating ("createBasicEnemy", 4, 10);
			InvokeRepeating ("createBasicEnemy", 26, 10);
			InvokeRepeating ("createToughEnemy", 40, 8);
			break;
		case 3:
			InvokeRepeating ("createBasicEnemy", 4, 10);
			InvokeRepeating ("createIceResistantEnemy", 26, 10);
			InvokeRepeating ("createToughEnemy", 40, 8);
			break;
		case 4:
			InvokeRepeating ("createBasicEnemy", 4, 10);
			InvokeRepeating ("createToughEnemy", 26, 10);
			InvokeRepeating ("createFastEnemy", 40, 8);
			break;
		case 5: // special
			createImmovableHero(powerConverter, 1, 1);
			createImmovableHero(powerConverter, 1, 3);
			createImmovableHero(iceConverter, 0, 1);
			createImmovableHero(iceConverter, 0, 3);
			createImmovableHero(speedConverter, 2, 0);
			createImmovableHero(speedConverter, 2, 2);
			createImmovableHero(speedConverter, 2, 4);

			InvokeRepeating ("createToughEnemy", 4, 10);
			InvokeRepeating ("createFastEnemy", 26, 12);
			InvokeRepeating ("createIceResistantEnemy", 40, 8);
			InvokeRepeating ("createToughEnemy", 70, 10);
			InvokeRepeating ("createFastEnemy", 80, 10);
			break;
		case 6:
			InvokeRepeating ("createBasicEnemy", 4, 10);
			InvokeRepeating ("createFastEnemy", 26, 10);
			InvokeRepeating ("createFastToughEnemy", 40, 8);
			break;
		case 7:
			InvokeRepeating ("createBasicEnemy", 4, 10);
			InvokeRepeating ("createFastEnemy", 26, 10);
			InvokeRepeating ("createFastToughEnemy", 40, 8);
			InvokeRepeating ("createIceResistantEnemy", 70, 10);
			break;
		case 8:
			InvokeRepeating ("createBasicEnemy", 4, 10);
			InvokeRepeating ("createFastEnemy", 26, 10);
			InvokeRepeating ("createToughEnemy", 40, 8);
			InvokeRepeating ("createFastToughEnemy", 70, 10);
			InvokeRepeating ("createIceResistantEnemy", 80, 12);
			break;
		default:
			break;
		}
	}

	private void spawnFinalWave() {
		switch (currentLevel) {
		case 1:
			createSingleBasicEnemy (0);
			createSingleBasicEnemy (1);
			createSingleBasicEnemy (2);
			createSingleBasicEnemy (3);
			createSingleBasicEnemy (4);
			break;
		case 2:
			createSingleBasicEnemy (0);
			createSingleBasicEnemy (1);
			createSingleBasicEnemy (2);
			createSingleBasicEnemy (3);
			createSingleBasicEnemy (4);

			StartCoroutine(createToughEnemyDelayed (0));
			StartCoroutine(createToughEnemyDelayed (1));
			StartCoroutine(createToughEnemyDelayed (2));
			StartCoroutine(createToughEnemyDelayed (3));
			StartCoroutine(createToughEnemyDelayed (4));
			break;
		case 3:
			createSingleBasicEnemy (0);
			createSingleBasicEnemy (1);
			createSingleBasicEnemy (2);
			createSingleBasicEnemy (3);
			createSingleBasicEnemy (4);

			StartCoroutine(createIceResistantEnemyDelayed (0));
			StartCoroutine(createIceResistantEnemyDelayed (1));
			StartCoroutine(createIceResistantEnemyDelayed (2));
			StartCoroutine(createIceResistantEnemyDelayed (3));
			StartCoroutine(createIceResistantEnemyDelayed (4));

			StartCoroutine(createToughEnemyDelayed (0, 2.5f));
			StartCoroutine(createToughEnemyDelayed (2, 2.5f));
			StartCoroutine(createToughEnemyDelayed (4, 2.5f));
			break;
		case 4:
			createSingleBasicEnemy (0);
			createSingleBasicEnemy (1);
			createSingleBasicEnemy (2);
			createSingleBasicEnemy (3);
			createSingleBasicEnemy (4);

			StartCoroutine(createFastEnemyDelayed (0));
			StartCoroutine(createFastEnemyDelayed (1));
			StartCoroutine(createFastEnemyDelayed (2));
			StartCoroutine(createFastEnemyDelayed (3));
			StartCoroutine(createFastEnemyDelayed (4));

			StartCoroutine(createToughEnemyDelayed (0, 2.5f));
			StartCoroutine(createToughEnemyDelayed (2, 2.5f));
			StartCoroutine(createToughEnemyDelayed (4, 2.5f));
			break;
		case 5:
			createSingleFastEnemy (0);
			createSingleFastEnemy (1);
			createSingleFastEnemy (2);
			createSingleFastEnemy (3);
			createSingleFastEnemy (4);

			StartCoroutine(createIceResistantEnemyDelayed (0));
			StartCoroutine(createIceResistantEnemyDelayed (1));
			StartCoroutine(createIceResistantEnemyDelayed (2));
			StartCoroutine(createIceResistantEnemyDelayed (3));
			StartCoroutine(createIceResistantEnemyDelayed (4));

			StartCoroutine(createToughEnemyDelayed (0, 2.5f));
			StartCoroutine(createToughEnemyDelayed (1, 2.5f));
			StartCoroutine(createToughEnemyDelayed (2, 2.5f));
			StartCoroutine(createToughEnemyDelayed (3, 2.5f));
			StartCoroutine(createToughEnemyDelayed (4, 2.5f));
			break;
		case 6:
			createSingleFastEnemy (0);
			createSingleFastEnemy (1);
			createSingleFastEnemy (2);
			createSingleFastEnemy (3);
			createSingleFastEnemy (4);

			StartCoroutine(createBasicEnemyDelayed (0));
			StartCoroutine(createBasicEnemyDelayed (1));
			StartCoroutine(createBasicEnemyDelayed (2));
			StartCoroutine(createBasicEnemyDelayed (3));
			StartCoroutine(createBasicEnemyDelayed (4));

			StartCoroutine(createFastToughEnemyDelayed (0, 2.5f));
			StartCoroutine(createFastToughEnemyDelayed (2, 2.5f));
			StartCoroutine(createFastToughEnemyDelayed (4, 2.5f));
			break;
		case 7:
			createSingleFastEnemy (0);
			createSingleFastEnemy (1);
			createSingleFastEnemy (2);
			createSingleFastEnemy (3);
			createSingleFastEnemy (4);

			StartCoroutine(createIceResistantEnemyDelayed (0));
			StartCoroutine(createIceResistantEnemyDelayed (1));
			StartCoroutine(createIceResistantEnemyDelayed (2));
			StartCoroutine(createIceResistantEnemyDelayed (3));
			StartCoroutine(createIceResistantEnemyDelayed (4));

			StartCoroutine(createFastToughEnemyDelayed (0, 2.5f));
			StartCoroutine(createFastToughEnemyDelayed (1, 2.5f));
			StartCoroutine(createFastToughEnemyDelayed (2, 2.5f));
			StartCoroutine(createFastToughEnemyDelayed (3, 2.5f));
			StartCoroutine(createFastToughEnemyDelayed (4, 2.5f));
			break;
		case 8:
			createSingleFastEnemy (0);
			createSingleFastEnemy (1);
			createSingleFastEnemy (2);
			createSingleFastEnemy (3);
			createSingleFastEnemy (4);

			StartCoroutine(createIceResistantEnemyDelayed (0));
			StartCoroutine(createIceResistantEnemyDelayed (1));
			StartCoroutine(createIceResistantEnemyDelayed (2));
			StartCoroutine(createIceResistantEnemyDelayed (3));
			StartCoroutine(createIceResistantEnemyDelayed (4));

			StartCoroutine(createFastToughEnemyDelayed (0, 2.5f));
			StartCoroutine(createFastToughEnemyDelayed (1, 2.5f));
			StartCoroutine(createFastToughEnemyDelayed (2, 2.5f));
			StartCoroutine(createFastToughEnemyDelayed (3, 2.5f));
			StartCoroutine(createFastToughEnemyDelayed (4, 2.5f));

			StartCoroutine(createToughEnemyDelayed (0, 4.0f));
			StartCoroutine(createToughEnemyDelayed (1, 4.0f));
			StartCoroutine(createToughEnemyDelayed (2, 4.0f));
			StartCoroutine(createToughEnemyDelayed (3, 4.0f));
			StartCoroutine(createToughEnemyDelayed (4, 4.0f));
			break;
		default:
			break;
		}
	}

	public void stopSpawning() {
		CancelInvoke ("createBasicEnemy");
		CancelInvoke ("createFastEnemy");
		CancelInvoke ("createIceResistantEnemy");
		CancelInvoke ("createToughEnemy");
		CancelInvoke ("createFastToughEnemy");
	}

	// end game

	public void endGame(bool win) {
		stopSpawning ();

		if (win) {
			lastWaveInitialized = true;
			finalWaveCanvas.SetActive (true);
			spawnFinalWave ();
		} else {
			lose ();
		}
	}

	void Update() {
		if (lastWaveInitialized && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
			lastWaveInitialized = false;
			win ();
		}
	}

	private void win() {
		if (didFinishGame == false) {
			didFinishGame = true;

			winCanvas.SetActive (true);
			int actualScore = GameObject.FindGameObjectWithTag ("Score").GetComponent<ScoreScript> ().getScore ();
			winScore.GetComponent<WinScoreScript> ().setScore (actualScore);
			clearBoard ();
		}
	}

	private void lose() {
		if (didFinishGame == false) {
			didFinishGame = true;

			loseCanvas.SetActive (true);
			clearBoard ();
		}
	}

	private void clearBoard() {
		hudCanvas.SetActive (false);
		tutorialCanvas.SetActive (false);
		finalWaveCanvas.SetActive (false);

		deallocateObjects ();
	}

	private void deallocateObjects() {
		GameObject[] healthBars = GameObject.FindGameObjectsWithTag("HealthBar");
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] heroes = GameObject.FindGameObjectsWithTag("Hero");
		GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");

		foreach (GameObject healthBar in healthBars) {
			Destroy (healthBar);
		}

		foreach (GameObject enemy in enemies) {
			Destroy (enemy);
		}

		foreach (GameObject hero in heroes) {
			Destroy (hero);
		}

		foreach (GameObject pickup in pickups) {
			Destroy (pickup);
		}
	}

	// Creation

	IEnumerator createBasicEnemyDelayed(int row, float delay = 1.0f) {
		yield return new WaitForSeconds(delay);
		createSingleBasicEnemy (row);
	}

	IEnumerator createFastEnemyDelayed(int row, float delay = 1.0f) {
		yield return new WaitForSeconds(delay);
		createSingleFastEnemy (row);
	}

	IEnumerator createIceResistantEnemyDelayed(int row, float delay = 1.0f) {
		yield return new WaitForSeconds(delay);
		createSingleIceResistantEnemy (row);
	}

	IEnumerator createToughEnemyDelayed(int row, float delay = 1.0f) {
		yield return new WaitForSeconds(delay);
		createSingleToughEnemy (row);
	}

	IEnumerator createFastToughEnemyDelayed(int row, float delay = 1.0f) {
		yield return new WaitForSeconds(delay);
		createSingleFastToughEnemy (row);
	}

	void createBasicEnemy() {
		createSingleBasicEnemy ();
	}

	void createFastEnemy() {
		createSingleFastEnemy ();
	}

	void createIceResistantEnemy() {
		createSingleIceResistantEnemy ();
	}

	void createToughEnemy() {
		createSingleToughEnemy ();
	}

	void createFastToughEnemy() {
		createSingleFastToughEnemy ();
	}

	void createSingleBasicEnemy(int row = -1) {
		if (row <= -1 || row >= 5) {
			GameObject enemy = (GameObject)Instantiate (basicEnemy, new Vector2 (9.5f, StageScript.rows [Random.Range (0, (int)StageScript.numRows)] - 0.25f), Quaternion.identity);
			enemy.GetComponent<BasicEnemyScript> ().board = this.gameObject;
		} else {
			GameObject enemy = (GameObject)Instantiate (basicEnemy, new Vector2 (9.5f, StageScript.rows [row] - 0.25f), Quaternion.identity);
			enemy.GetComponent<BasicEnemyScript> ().board = this.gameObject;
		}
	}

	void createSingleFastEnemy(int row = -1) {
		if (row <= -1 || row >= 5) {
			GameObject enemy = (GameObject)Instantiate (fastEnemy, new Vector2 (9.5f, StageScript.rows [Random.Range (0, (int)StageScript.numRows)] - 0.05f), Quaternion.identity);
			enemy.GetComponent<BasicEnemyScript> ().board = this.gameObject;
		} else {
			GameObject enemy = (GameObject)Instantiate (fastEnemy, new Vector2 (9.5f, StageScript.rows [row] - 0.05f), Quaternion.identity);
			enemy.GetComponent<BasicEnemyScript> ().board = this.gameObject;
		}
	}

	void createSingleIceResistantEnemy(int row = -1) {
		if (row <= -1 || row >= 5) {
			GameObject enemy = (GameObject)Instantiate (iceResistantEnemy, new Vector2 (9.5f, StageScript.rows [Random.Range (0, (int)StageScript.numRows)]), Quaternion.identity);
			enemy.GetComponent<BasicEnemyScript> ().board = this.gameObject;
		} else {
			GameObject enemy = (GameObject)Instantiate (iceResistantEnemy, new Vector2 (9.5f, StageScript.rows [row]), Quaternion.identity);
			enemy.GetComponent<BasicEnemyScript> ().board = this.gameObject;
		}
	}

	void createSingleToughEnemy(int row = -1) {
		if (row <= -1 || row >= 5) {
			GameObject enemy = (GameObject)Instantiate (toughEnemy, new Vector2 (9.5f, StageScript.rows [Random.Range (0, (int)StageScript.numRows)] - 0.25f), Quaternion.identity);
			enemy.GetComponent<BasicEnemyScript> ().board = this.gameObject;
		} else {
			GameObject enemy = (GameObject)Instantiate (toughEnemy, new Vector2 (9.5f, StageScript.rows [row] - 0.25f), Quaternion.identity);
			enemy.GetComponent<BasicEnemyScript> ().board = this.gameObject;
		}
	}

	void createSingleFastToughEnemy(int row = -1) {
		if (row <= -1 || row >= 5) {
			GameObject enemy = (GameObject)Instantiate (fastToughEnemy, new Vector2 (9.5f, StageScript.rows [Random.Range (0, (int)StageScript.numRows)] + 0.05f), Quaternion.identity);
			enemy.GetComponent<BasicEnemyScript> ().board = this.gameObject;
		} else {
			GameObject enemy = (GameObject)Instantiate (fastToughEnemy, new Vector2 (9.5f, StageScript.rows [row] + 0.05f), Quaternion.identity);
			enemy.GetComponent<BasicEnemyScript> ().board = this.gameObject;
		}
	}

	// POWER UPS

	void createImmovableHero(GameObject go, int posX, int posY) {
		GameObject hero = (GameObject)Instantiate (go, Vector2.zero, Quaternion.identity);
		hero.GetComponent<MovableHeroScript> ().moveToSlot (posX, posY, true);
		hero.GetComponent<MovableHeroScript> ().buildBench = buildBench;
		hero.GetComponent<MovableHeroScript> ().immovable = true;
	}

}
