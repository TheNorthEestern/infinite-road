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


		if ( _rb.velocity.x > 2.0f ) 
		{

			if ( moveHorizontal < 0 ) {
				lane = left;
			} 

			if ( moveHorizontal > 0 ) {
				lane = right;
			}

			if ( lane == left ) {

				float distance = Mathf.Abs(leftBound - _rb.transform.position.z);
				float v = (Mathf.Sqrt(distance)/ 10) * -1.0f;
				if (_rb.transform.position.z < leftBound) {
					_rb.transform.Translate(new Vector3(v, 0, 0));
				}
			}

			if ( lane == right) {

				float distance = Mathf.Abs(rightBound - _rb.transform.position.z);
				float v = (Mathf.Sqrt(distance)/ 10) * 1.0f;
				if (_rb.transform.position.z > rightBound) {
					_rb.transform.Translate(new Vector3(v, 0, 0));
				}
			}
//			int v = 1;
//
//			if(lane==left) v=-1; 
//			if(lane==right) v=1;
//
//			Vector3 wheelControl = new Vector3(v, 0, 0);
//			wheelControl = Vector3.ClampMagnitude(wheelControl, 0.9f);
//			_rb.transform.Translate(wheelControl);
//
//			Debug.Log ("LANE -> " + lane);

			/* Vector3 wheelControl = new Vector3(moveHorizontal, 0, 0);
			wheelControl = Vector3.ClampMagnitude(wheelControl, 0.1f);
			_rb.transform.Translate(wheelControl);*/
		}

	}

}
