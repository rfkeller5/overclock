using UnityEngine;
using System.Collections;

public class IcePowerConverterScript : BasePowerConverterScript {

	override protected void Awake () {
		base.Awake ();

		heroType = HeroType.ICE_POWER_CONVERTER;

		// X pattern
		powerPositions.Add(new IntVector2 (-1, -1));
		powerPositions.Add(new IntVector2 (1, -1));
		powerPositions.Add(new IntVector2 (-1, 1));
		powerPositions.Add(new IntVector2 (1, 1));
	}

}
