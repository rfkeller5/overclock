using UnityEngine;
using System.Collections;

public class PowerBoxScript : HeroScript {

	public GameObject fireball;

	override protected void removeFromBoard() {
		Instantiate (fireball, new Vector3 (transform.position.x, transform.position.y, 0.0f), Quaternion.identity);

		base.removeFromBoard ();
	}

}
