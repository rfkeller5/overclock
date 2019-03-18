using UnityEngine;
using System.Collections;

public class SpeedPowerConverterScript : BasePowerConverterScript {

	override protected void Awake () {
		base.Awake ();

		heroType = HeroType.SPEED_POWER_CONVERTER;

		// Cone pattern
		powerPositions.Add(new IntVector2 (1, -1));
		powerPositions.Add(new IntVector2 (1, 0));
		powerPositions.Add(new IntVector2 (1, 1));
	}
}
