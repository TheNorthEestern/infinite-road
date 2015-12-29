using UnityEngine;
using System.Collections;

public class RoadCleanupBehavior : MonoBehaviour {

	// OnBecameInvisible is called when the object is out of the camera's
	// view frustum
	void OnBecameInvisible () {
		Invoke ("Discard", 1f);
	}
	
	void Discard() {
		// Destroy (this.gameObject);
		gameObject.SetActive(false);
	}
}
