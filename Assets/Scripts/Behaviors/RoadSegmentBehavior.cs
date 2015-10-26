using UnityEngine;
using System.Collections;

public class RoadSegmentBehavior : MonoBehaviour {
	// OnBecameVisible is called when the object enters the camera's
	// view frustum
	void OnBecameVisible () {

	}

	// OnBecameInvisible is called when the object is out of the camera's
	// view frustum
	void OnBecameInvisible () {
		Invoke ("Discard", 5);
	}

	void Discard() {
		// Destroy (this.gameObject);
	}

}