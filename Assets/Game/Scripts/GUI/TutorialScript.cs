using UnityEngine;
using System.Collections;

public enum TutorialEvent {
	BEGIN = 0,
	BASIC_MOVE,
	OVERCLOCK,
	FINISH
}

public class TutorialScript : MonoBehaviour {

	public GameObject trooper;
	public GameObject powerGenerator;
	public GameObject commando;
	public GameObject basicEnemy;

	public GameObject moveTrooperText;
	public GameObject overclockTrooperText;

	public GameObject buildBench;
	public GameObject board;
	public GameObject tutorialCanvas;
	public GameObject levelCanvas;
	public GameObject levelText;

	public GameObject barrel1;
	public GameObject barrel2;
	public GameObject barrel3;
	public GameObject barrel4;
	public GameObject barrel5;

	private TutorialEvent tutorialEvent = TutorialEvent.BEGIN;
	private GameObject powerConverter;
	private GameObject privateCommando;
	private int enemyRepeateCounter = 0;

	void Start () {
		createHero (trooper, 0, 3);
		createBasicEnemy (1);
		tutorialEvent = TutorialEvent.BASIC_MOVE;
		setBarrelsActive (false);
		enemyRepeateCounter = 0;
	}

	void FixedUpdate() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if (enemies.Length == 0) {
			switch (tutorialEvent) {
			case TutorialEvent.BASIC_MOVE:
				powerConverter = createHero (powerGenerator, 1, 3);
				powerConverter.GetComponent<MovableHeroScript> ().immovable = true;
				privateCommando = createHero (commando, 6, 3);
				privateCommando.GetComponent<MovableHeroScript> ().immovable = true;

				InvokeRepeating ("createEnemies", 0.0f, 2.5f);

				moveTrooperText.SetActive (false);
				overclockTrooperText.SetActive (true);
				tutorialEvent = TutorialEvent.OVERCLOCK;
				break;
			case TutorialEvent.OVERCLOCK:
				powerConverter.GetComponent<MovableHeroScript> ().immovable = false;
				privateCommando.GetComponent<MovableHeroScript> ().immovable = false;
				tutorialEvent = TutorialEvent.FINISH;
				setBarrelsActive (true);

				tutorialCanvas.SetActive (false);
				levelCanvas.SetActive (true);
				levelText.GetComponent<LevelDisplayScript> ().showLevel (1);
				break;
			default:
				break;
			}
		}
	}


	GameObject createHero(GameObject go, int posX, int posY) {
		GameObject hero = (GameObject)Instantiate (go, Vector2.zero, Quaternion.identity);
		hero.GetComponent<MovableHeroScript> ().moveToSlot (posX, posY, true);
		hero.GetComponent<MovableHeroScript> ().buildBench = this.buildBench;
		return hero;
	}

	void createBasicEnemy(int row) {
		GameObject enemy = (GameObject)Instantiate (basicEnemy, new Vector2 (9.5f, StageScript.rows [row] - 0.25f), Quaternion.identity);
		enemy.GetComponent<BasicEnemyScript> ().board = this.board;
		enemy.GetComponent<BasicEnemyScript> ().dropCurrency = false;
	}

	void createEnemies() {
		createBasicEnemy (3);
		++this.enemyRepeateCounter;
		if (this.enemyRepeateCounter >= 4) {
			CancelInvoke ("createEnemies");
		}
	}

	void setBarrelsActive(bool active) {
		barrel1.SetActive (active);
		barrel1.GetComponent<HeroScript> ().activeHealthBar.SetActive(active);

		barrel2.SetActive (active);
		barrel2.GetComponent<HeroScript> ().activeHealthBar.SetActive(active);

		barrel3.SetActive (active);
		barrel3.GetComponent<HeroScript> ().activeHealthBar.SetActive(active);

		barrel4.SetActive (active);
		barrel4.GetComponent<HeroScript> ().activeHealthBar.SetActive(active);

		barrel5.SetActive (active);
		barrel5.GetComponent<HeroScript> ().activeHealthBar.SetActive(active);
	}
}
