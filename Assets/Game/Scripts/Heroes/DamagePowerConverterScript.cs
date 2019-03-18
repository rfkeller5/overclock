using UnityEngine;
using System.Collections;

public class DamagePowerConverterScript : BasePowerConverterScript {

	override protected void Awake () {
		base.Awake ();

		heroType = HeroType.DAMAGE_POWER_CONVERTER;

		// cross pattern
		powerPositions.Add(new IntVector2 (0, -1));
		powerPositions.Add(new IntVector2 (1, 0));
		powerPositions.Add(new IntVector2 (0, 1));
		powerPositions.Add(new IntVector2 (-1, 0));
	}
}
