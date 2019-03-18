using UnityEngine;
using System.Collections;

public enum HeroType {
	NONE = 0,
	GUNNER,
	BLOCKER,
	DAMAGE_POWER_CONVERTER,
	ICE_POWER_CONVERTER,
	SPEED_POWER_CONVERTER
}

public class MovableHeroScript : HeroScript {
	
	public GameObject buildBench;

	const int notPlaced = -5; // -5 means on-bench

	protected HeroType heroType;
	public int colPos { get; protected set; }
	public int rowPos { get; protected set; }

	public bool isDragging { get; protected set; }
	public bool isOnBench = true;
	public bool immovable = false;
	public bool delayedImmovable = false;

	public AudioSource dieSound;

	// life-cycle

	override protected void Awake() {
		base.Awake ();

		colPos = notPlaced;
		rowPos = notPlaced;
	}

	virtual protected void Start() {
		this.isDragging = false;
	}

	// board management

	override protected void removeFromBoard() {
		bool removeImmediately = willRemoveFromBoard ();
		StageScript.emptySlot (colPos, rowPos);

		if (dieSound) {
			dieSound.Play ();
		}

		if (removeImmediately) {
			base.removeFromBoard ();
		}
	}

	virtual protected bool willRemoveFromBoard() {
		return true;
	}

	virtual protected void didMoveToSlot(int col, int row, int previousCol, int previousRow, bool earlyCreation) {
		if (isOnBench && !earlyCreation) {
			buildBench.GetComponent<BuildManagerScript>().removeFromBench (this.gameObject);
			isOnBench = false;
			if (delayedImmovable) {
				immovable = delayedImmovable;
			}
		}
	}

	public void moveToSlot(int c, int r, bool earlyCreation = false) {
		int previousCol = colPos;
		int previousRow = rowPos;
		bool slotEmpty = false;
		if (StageScript.isSlotEmpty (c, r)) {
			slotEmpty = true;
			StageScript.addCharacterToSlot (c, r, heroType);

			if (colPos >= 0 && rowPos >= 0) {
				StageScript.emptySlot (colPos, rowPos);
			}

			colPos = c; rowPos = r;
		}

		if (colPos < 0 || rowPos < 0) {
			buildBench.GetComponent<BuildManagerScript>().updatePositions();
		} else {
			transform.position = new Vector2 (StageScript.cols [colPos], StageScript.rows [rowPos]);
			if (slotEmpty) {
				didMoveToSlot (c, r, previousCol, previousRow, earlyCreation);
			}
		}
	}

	void OnMouseDrag() {
		if (paused || immovable) {
			return;
		}
		isDragging = true;
		CancelInvoke ();

		transform.position = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f));
		transform.position = new Vector3 (transform.position.x, transform.position.y, 0.0f);
	}

	void OnMouseUp() {
		if (immovable) {
			return;
		}

		isDragging = false;

		bool foundMatch = false;
		for (int c = 0; c < StageScript.cols.Length; ++c) {
			for (int r = 0; r < StageScript.rows.Length; ++r) {
				float xDiff = transform.position.x - StageScript.cols [c];
				float yDiff = transform.position.y - StageScript.rows [r];

				if (xDiff >= (-StageScript.tileDimension * 0.5f) && (xDiff <= StageScript.tileDimension * 0.5f) &&
					yDiff >= (-StageScript.tileDimension * 0.5f) && (yDiff <= StageScript.tileDimension * 0.5f)) {

					moveToSlot (c, r);

					foundMatch = true;
					break;
				}
			}
			if (foundMatch) {
				break;
			}
		}

		if (!foundMatch) {
			if (isOnBench) {
				buildBench.GetComponent<BuildManagerScript>().updatePositions ();
			} else {
				moveToSlot (this.colPos, this.rowPos);
			}
		}
	}
}
