using UnityEngine;
using System.Collections;

public class InfoMenuScript : MonoBehaviour {

	public GameObject heroTabs;
	public GameObject enemyTabs;

	public GameObject trooperDetails;
	public GameObject commandoDetails;
	public GameObject powerDetails;
	public GameObject speedDetails;
	public GameObject iceDetails;

	public GameObject greenSlimeDetails;
	public GameObject pinkSlimeDetails;
	public GameObject yellowSprinterDetails;
	public GameObject blueSprinterDetails;
	public GameObject wormDetails;

	void Start () {
		goToHeroes ();
		showDetails (0);
	}

	public void goToHeroes() {
		heroTabs.SetActive (true);
		enemyTabs.SetActive (false);
	}

	public void goToEnemies() {
		heroTabs.SetActive (false);
		enemyTabs.SetActive (true);
	}

	void hideAllDetails() {
		trooperDetails.SetActive (false);
		commandoDetails.SetActive (false);
		powerDetails.SetActive (false);
		speedDetails.SetActive (false);
		iceDetails.SetActive (false);

		greenSlimeDetails.SetActive (false);
		pinkSlimeDetails.SetActive (false);
		yellowSprinterDetails.SetActive (false);
		blueSprinterDetails.SetActive (false);
		wormDetails.SetActive (false);
	}

	public void showDetails(int type) {
		hideAllDetails ();

		switch (type) {
		case 0: // trooper
			trooperDetails.SetActive (true);
			break;
		case 1: // commando
			commandoDetails.SetActive (true);
			break;
		case 2: // power
			powerDetails.SetActive (true);
			break;
		case 3: // speed
			speedDetails.SetActive (true);
			break;
		case 4: // ice
			iceDetails.SetActive (true);
			break;
		case 5: // basic enemy
			greenSlimeDetails.SetActive (true);
			break;
		case 6: // tough enemy
			pinkSlimeDetails.SetActive (true);
			break;
		case 7: // fast enemy
			yellowSprinterDetails.SetActive (true);
			break;
		case 8: // fast tough enemy
			blueSprinterDetails.SetActive (true);
			break;
		case 9: // ice resistant enemy
			wormDetails.SetActive (true);
			break;
		default:
			break;
		}
	}
}
