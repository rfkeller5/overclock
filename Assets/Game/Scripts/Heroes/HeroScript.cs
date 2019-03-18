using UnityEngine;
using System.Collections;

public class HeroScript : MonoBehaviour {

	public int health;
	private int startingHealth;

	public GameObject healthBar;
	public GameObject activeHealthBar;

	private Color redColor = new Color (255, 0, 0);
	private Color yellowColor = new Color (255, 255, 0);

	public bool isDying = false;

	protected bool paused = false;

	virtual protected void Awake () {
		startingHealth = health;

		activeHealthBar = (GameObject)Instantiate (healthBar, new Vector2 (transform.position.x, transform.position.y + 0.75f), Quaternion.identity);
	}

	void OnPauseGame() {
		paused = true;
	}

	void OnResumeGame() {
		paused = false;
	}
	
	// Update is called once per frame
	virtual protected void Update () {
		if (paused) {
			return;
		}
		activeHealthBar.transform.position = new Vector2 (transform.position.x, transform.position.y + 0.75f);

		if (this.health <= 0) {
			isDying = true;
			removeFromBoard ();
		}

		float healthPercentage = (float)health / (float)startingHealth;
		activeHealthBar.transform.localScale = new Vector3 (healthPercentage, 1.0f, 1.0f);
		if (healthPercentage <= 0.5f) {
			activeHealthBar.GetComponent<SpriteRenderer> ().color = redColor;
		} else if (healthPercentage <= 0.75f) {
			activeHealthBar.GetComponent<SpriteRenderer> ().color = yellowColor;
		}
	}

	public bool takeDamage(int damage) {
		this.health -= damage;
		return this.health <= 0;
	}

	virtual protected void removeFromBoard() {
		die ();
	}

	protected void die() {
		Destroy (activeHealthBar);
		Destroy (this.gameObject);
	}
}
