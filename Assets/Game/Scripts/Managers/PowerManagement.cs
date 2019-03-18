using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct UIntVector2 {
	
	public uint x; 
	public uint y;

	public UIntVector2(uint x, uint y) {
		this.x = x;
		this.y = y;
	}

}

public struct IntVector2 {

	public int x; 
	public int y;

	public IntVector2(int x, int y) {
		this.x = x;
		this.y = y;
	}

}

public class PowerManagement : MonoBehaviour {

	static List<HeroType>[,] powerSlots = new List<HeroType>[StageScript.numCols,StageScript.numRows];

	void Start() {
		for (int c = 0; c < StageScript.cols.Length; ++c) {
			for (int r = 0; r < StageScript.rows.Length; ++r) {
				powerSlots [c, r] = new List<HeroType> ();
			}
		}
	}

	public static List<HeroType> getPowerList(uint posX, uint posY) {
		return powerSlots [posX, posY];
	}

	public static void assignPower(HeroType type, uint posX, uint posY) {
		powerSlots [posX, posY].Add (type);
		StageScript.colorTile (type, posX, posY);
	}

	public static void removePower(HeroType type, uint posX, uint posY) {
		powerSlots [posX, posY].Remove (type);

		int count = powerSlots [posX, posY].Count;

		if (count == 0) {
			StageScript.colorTile (HeroType.NONE, posX, posY);
		} else {
			StageScript.colorTile (powerSlots [posX, posY][count - 1], posX, posY);
		}
	}

	public static void clearAllPower() {
		for (uint c = 0; c < StageScript.cols.Length; ++c) {
			for (uint r = 0; r < StageScript.rows.Length; ++r) {
				powerSlots[c, r].Clear();
				StageScript.colorTile (HeroType.NONE, c, r);
			}
		}
	}

}
