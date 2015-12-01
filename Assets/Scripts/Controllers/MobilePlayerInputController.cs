﻿// #if !UNITY_STANDALONE || !UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class MobilePlayerInputController : PlayerController {

	private IEnumerator Reorient() {
		moveHorizontal = 0;
		yield return null;
	}
	
	public override void FixedUpdate () 
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
		    Application.platform == RuntimePlatform.Android) {
			base.FixedUpdate();

			movement = Vector3.right;
			moveVertical  = 1.0f;

			if ( Input.touchCount > 0 ) {
				// Touch touch = Input.touches[0];
			   if ( Input.touchCount == 1 ) {
				foreach  (Touch touch in Input.touches) {
					if (touch.position.x < (Screen.width / 2) && (touch.position.y < (Screen.height / 2))) {
						moveHorizontal = -1.0f;
					} 
					
					else if ((touch.position.x > (Screen.width / 2)) && (touch.position.y < (Screen.height / 2))) {
						moveHorizontal = 1.0f;
					}
				}
			  }
				if ( Input.touchCount == 2 ) {
					movement = Vector3.zero;
					moveVertical = 1.0f;
				}
			} else {
				moveHorizontal = 0.0f;
			}
			
		}
	} 
}
// #endif