using UnityEngine;
using System.Collections;

public class RoadCleanupBehavior : MonoBehaviour {

	// OnBecameInvisible is called when the object is out of the camera's
	// view frustum
	void OnBecameInvisible () {
		Invoke ("Discard", 2);
	}
	
	void Discard() {
		Destroy (this.gameObject);
	}
}
