using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasePowerConverterScript : MovableHeroScript {

	protected List<IntVector2> powerPositions = new List<IntVector2>();

	override protected bool willRemoveFromBoard() {
		removePowerToTilesFrom (this.colPos, this.rowPos);

		return base.willRemoveFromBoard ();
	}

	override protected void didMoveToSlot(int col, int row, int previousCol, int previousRow, bool earlyCreation) {
		base.didMoveToSlot (col, row, previousCol, previousRow, earlyCreation);

		if (!earlyCreation) {
			removePowerToTilesFrom (previousCol, previousRow);
		}

		addPowerToTilesFrom (col, row);
	}

	private void addPowerToTilesFrom(int col, int row) {
		for (int i = 0; i < powerPositions.Count; ++i) {
			int xPos = powerPositions [i].x + col;
			int yPos = powerPositions [i].y + row;
			if (xPos >= 0 && xPos < StageScript.numCols && yPos >= 0 && yPos < StageScript.numRows) {
				PowerManagement.assignPower (heroType, (uint)xPos, (uint)yPos);
			}
		}
	}

	private void removePowerToTilesFrom(int col, int row) {
		for (int i = 0; i < powerPositions.Count; ++i) {
			int xPos = powerPositions [i].x + col;
			int yPos = powerPositions [i].y + row;
			if (xPos >= 0 && xPos < StageScript.numCols && yPos >= 0 && yPos < StageScript.numRows) {
				PowerManagement.removePower (heroType, (uint)xPos, (uint)yPos);
			}
		}
	}
}
