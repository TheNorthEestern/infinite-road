using UnityEngine;
using System.Collections;

public class RoadSegmentBehavior : MonoBehaviour {
	// OnBecameVisible is called when the object enters the camera's
	// view frustum
	void OnBecameVisible () {
		Debug.Log (transform.name);
		Debug.Log ("Hello");
	}
	
	// OnBecameInvisible is called when the object is out of the camera's
	// view frustum
	void OnBecameInvisible () {
		if (gameObject.name.Contains ("Clone")) {
			Destroy (this.gameObject);
		}
	}
}
