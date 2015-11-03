using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class PlayerInputController : PlayerController {
	
	public override void FixedUpdate() 
	{
		base.FixedUpdate();
		moveVertical = Input.GetAxis ("Vertical");
		moveHorizontal = Input.GetAxis ("Horizontal");

		movement = new Vector3 (moveVertical, 0.0f, 0.0f);

		if ( _rb.velocity.x > 0 ) {
			Vector3 wheelControl = new Vector3(moveHorizontal, 0, 0);
			wheelControl = Vector3.ClampMagnitude(wheelControl, 0.1f);
			_rb.transform.Translate(wheelControl);
		}

	}
}
