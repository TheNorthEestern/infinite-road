#if !UNITY_IOS || !UNITY_ANDROID
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class DesktopPlayerInputController : PlayerController {
	
	public override void FixedUpdate() 
	{
		if ( Application.platform != RuntimePlatform.IPhonePlayer ||
		     Application.platform != RuntimePlatform.Android ) {
			base.FixedUpdate();
			moveVertical = Input.GetAxis ("Vertical");
			moveHorizontal = Input.GetAxis ("Horizontal");
			movement = new Vector3 (moveVertical, 0.0f, 0.0f);
		}
	}

}
#endif