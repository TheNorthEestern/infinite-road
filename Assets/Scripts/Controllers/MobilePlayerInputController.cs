﻿#if !UNITY_STANDALONE || !UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class MobilePlayerInputController : PlayerController {

	private Vector2 _touchOrigin = -Vector2.zero;
	private float yVelocity = 0.0f;

	private IEnumerator Reorient() {
		moveHorizontal = 0;
		yield return null;
	}
	
	protected override void FixedUpdate () 
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer || 
		    Application.platform == RuntimePlatform.Android) {
			base.FixedUpdate();

			movement = Vector3.right;
			moveVertical  = 1.0f;

			/*if ( Input.touchCount == 0 ) {
				moveHorizontal = Mathf.Lerp(moveHorizontal, 0, 0.3f);
				// moveHorizontal = Mathf.SmoothDamp(moveHorizontal, 0, ref yVelocity, 0.3f);
				// moveHorizontal = 0;
			}*/

			if ( Input.touchCount > 0 ) {

				Touch playerTouch = Input.touches[0];

				if (playerTouch.phase == TouchPhase.Began) {
					_touchOrigin = playerTouch.position;	
				}

				else if (playerTouch.phase == TouchPhase.Ended && _touchOrigin.x >= 0 ||
						 playerTouch.phase == TouchPhase.Moved && _touchOrigin.x >= 0) {

					Vector2 touchEnd = playerTouch.position;

					float x = touchEnd.x - _touchOrigin.x;
					float y = touchEnd.y - _touchOrigin.y;

					if (Mathf.Abs(x) > Mathf.Abs(y)) {
						moveHorizontal = (x > 0) ? 1 : -1;
					}

					StartCoroutine(Center());
					_touchOrigin.x = -1;
				}
			}


			if ( Input.touchCount == 2 ) {
				movement = Vector3.zero;
				moveVertical = -1.0f;
			}
		}
	} 

	public void Left() {
		moveHorizontal = -1.0f;
		StartCoroutine(Center());
	}

	public void Right() {
		moveHorizontal = 1.0f;
		StartCoroutine(Center());
	}

	private IEnumerator Center() {
		yield return new WaitForSeconds(0.25f);
		moveHorizontal = 0.0f;
	}
}
#endif