#if !UNITY_EDITOR || !UNITY_STANDALONE_OSX || !UNITY_STANDALONE_WIN || !UNITY_STANDALONE_LINUX
using UnityEngine;
using System.Collections;

public class MobilePlayerInputController : PlayerController {
			
	public override void FixedUpdate () 
	{
		base.FixedUpdate();
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
		    Application.platform == RuntimePlatform.Android) {

			movement = Vector3.right;
			moveVertical = 1.0f;

			if ( Input.touchCount > 0 ) {
				Touch touch = Input.touches[0];
			
				if (touch.position.x < Screen.width / 2) {
					moveHorizontal = -1.0f;
				} 

				else if (touch.position.x > Screen.width / 2) {
					moveHorizontal = 1.0f;
				}
			} 
			else {
				moveHorizontal = 0f;
			}
		
		}

	}
}
#endif