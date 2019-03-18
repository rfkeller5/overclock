using UnityEngine;
using System.Collections;

public class StageScript : MonoBehaviour {

	public GameObject tile;

	public const uint numCols = 8;
	public const uint numRows = 5;

	public static float[] cols = new float[numCols]; // x
	public static float[] rows = new float[numRows]; // y

	public const float tileDimension = 1.628f;

	static HeroType[,] occupancy = new HeroType[8, 5];
	static GameObject[,] tiles = new GameObject[8, 5];

	public const float startingColumn = -3.44f;
	public const float startingRow = 3.64f;

	private const int screenWidth = 16;
	private const int screenHeight = 12;


	void Start () {
		// create board
		for (int c = 0; c < cols.Length; ++c) {
			for (int r = 0; r < rows.Length; ++r) {
				// create positions
				cols [c] = startingColumn + (tileDimension * c);
				rows [r] = startingRow - (tileDimension * r);

				GameObject newTile = (GameObject)Instantiate (tile, new Vector3 (cols [c], rows [r], 1.0f), Quaternion.identity);
				newTile.transform.parent = gameObject.transform;

				tiles [c, r] = newTile;
				occupancy [c, r] = HeroType.NONE;
			}
		}
//		Application.targetFrameRate = 30;
	}

	// FPS COUNTER

//	float deltaTime = 0.0f;
//
//	void Update()
//	{
//		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
//	}
//
//	void OnGUI()
//	{
//		int w = Screen.width, h = Screen.height;
//
//		GUIStyle style = new GUIStyle();
//
//		Rect rect = new Rect(0, 0, w, h * 2 / 100);
//		style.alignment = TextAnchor.UpperLeft;
//		style.fontSize = h * 2 / 100;
//		style.normal.textColor = new Color (0.0f, 0.0f, 0.5f, 1.0f);
//		float msec = deltaTime * 1000.0f;
//		float fps = 1.0f / deltaTime;
//		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
//		GUI.Label(rect, text, style);
//	}

	// TILE MANAGEMENT

	public static void colorTile(HeroType heroType, uint col, uint row) {

		Color whiteColor = new Color (255, 255, 255);
		Color blueColor = new Color (0, 0, 255);
		Color redColor = new Color (255, 0, 0);
		Color yellowColor = new Color (255, 255, 0);

		switch (heroType) {
		case HeroType.NONE:
			tiles[col, row].GetComponent<SpriteRenderer> ().color = whiteColor;
			break;
		case HeroType.DAMAGE_POWER_CONVERTER:
			tiles[col, row].GetComponent<SpriteRenderer> ().color = redColor;
			break;
		case HeroType.ICE_POWER_CONVERTER:
			tiles[col, row].GetComponent<SpriteRenderer> ().color = blueColor;
			break;
		case HeroType.SPEED_POWER_CONVERTER:
			tiles[col, row].GetComponent<SpriteRenderer> ().color = yellowColor;
			break;
		default:
			tiles[col, row].GetComponent<SpriteRenderer> ().color = whiteColor;
			break;
		}
	}

	// SLOT MANAGEMENT

	public static bool isSlotEmpty(int col, int row) {
		return occupancy [col, row] == HeroType.NONE;
	}

	public static bool addCharacterToSlotAtPosition(float x, float y, HeroType type) {
		for (int c = 0; c < cols.Length; ++c) {
			for (int r = 0; r < rows.Length; ++r) {
				if (x == cols [c] && y == rows [r]) {
					return addCharacterToSlot (c, r, type);
				}
			}
		}

		return false;
	}

	public static bool addCharacterToSlot(int col, int row, HeroType type) {
		if (occupancy [col, row] == HeroType.NONE) {
			occupancy [col, row] = type;
			return true;
		}
		return false;
	}

	public static void emptySlot(int col, int row) {
		if (col >= 0 && row >= 0) {
			occupancy [col, row] = HeroType.NONE;
		}
	}

}
