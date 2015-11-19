#if !UNITY_STANDALONE || !UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class MobilePlayerInputController : PlayerController {
  private float fingerStartTime = 0.0f;
  private Vector2 fingerStartPos = Vector2.zero;
  private bool isSwipe = false;
  private float minimumSwipeDistance = 50.0f;
  private float maximumSwipeTime = 0.5f;

  private IEnumerator Reorient() {
    moveHorizontal = 0;
    yield return null;
  }

  public override void FixedUpdate () 
  {
    if (Application.platform == RuntimePlatform.IPhonePlayer || 
        Application.platform == RuntimePlatform.Android) {
      base.FixedUpdate();

      if ( Input.touchCount >= 0 ) {
        if (Input.touchCount == 0) {
          movement = Vector3.zero;
          moveVertical = -1.0f;
        }
        if (Input.touchCount == 1) {
			movement = Vector3.right;
			moveVertical = 1.0f;
			foreach (Touch touch in Input.touches) {
            switch (touch.phase) {

              case TouchPhase.Began:
                isSwipe = true;
                fingerStartTime = Time.time;
                fingerStartPos = touch.position;
                break;

              case TouchPhase.Canceled:
                isSwipe = false;
                break;

              case TouchPhase.Stationary:
							if ( touch.position.x < Screen.width / 2 ) moveHorizontal = -1.0f;
							if ( touch.position.x > Screen.width / 2 ) moveHorizontal = 1.0f;
                /*float gestureTime = Time.time - fingerStartTime;
                float gestureDist = (touch.position - fingerStartPos).magnitude;

                if (isSwipe && gestureTime < maximumSwipeTime && gestureDist > minimumSwipeDistance){
                  Vector2 direction = touch.position - fingerStartPos;
                  Vector2 swipeType = Vector2.zero;

                  if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
                    // the swipe is horizontal:
                    swipeType = Vector2.right * Mathf.Sign(direction.x);
                  } else {
                    // the swipe is vertical:
                    swipeType = Vector2.up * Mathf.Sign(direction.y);
                  }

                  if (direction.x < 0.0f && direction.y > 0.0f) {
                    // Diagonal Left
                    moveHorizontal = -1.0f;	
                  }	
                  if (direction.x > 0.0f && direction.y < 0.0f) {
                    // Diagonal Right
                    moveHorizontal = 1.0f;
                  }

                } */


                break;

              case TouchPhase.Ended:
                StartCoroutine(Reorient());
                break;

              default:
                break;

            }
          }
        }
        if (Input.touchCount == 2) {
          movement = Vector3.right;
          moveVertical = 1.0f;
        }
      }

    }
  } 
}
#endif
