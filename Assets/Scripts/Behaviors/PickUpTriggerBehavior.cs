using UnityEngine;
using System.Collections;

public class PickUpTriggerBehavior : MonoBehaviour {

	void Start() {
		GetComponent<Renderer>().enabled = false;
	}

	void OnTriggerEnter(Collider other) {
		if ( other.CompareTag ("Player") ) {
			Messenger.Broadcast (GameEvent.RAN_STOP_SIGN, MessengerMode.DONT_REQUIRE_LISTENER);
			Destroy (transform.parent.gameObject);
		}
	}
}
