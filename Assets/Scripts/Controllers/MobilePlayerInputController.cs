using UnityEngine;
using System.Collections;

public class MobilePlayerInputController : PlayerController {
			
	public override void FixedUpdate () 
	{
		base.FixedUpdate();
		movement = Vector3.zero;

		if (Application.platform == RuntimePlatform.IPhonePlayer || 
		    Application.platform == RuntimePlatform.Android) {

			if ( Input.touchCount >= 0 ) {
				switch(Input.touchCount) {
				case 0:
					movement = Vector3.zero;
					moveVertical = 0.0f;
					break;
					
				case 1:
					movement = Vector3.right;
					moveVertical = 1.0f;
					break;
					
				case 2:
					movement = Vector3.zero;
					moveVertical = -1.0f;
					break;
					
				default:
					break;
				}
			}

			if ( _rb.velocity.x > 0 ) {
				moveHorizontal = Input.acceleration.x;
				Vector3 accelerometerVector = new Vector3(Input.acceleration.x, 0, 0);
				accelerometerVector = Vector3.ClampMagnitude(accelerometerVector, 0.1f);
				_rb.transform.Translate(accelerometerVector);
			}
		}

	}
}