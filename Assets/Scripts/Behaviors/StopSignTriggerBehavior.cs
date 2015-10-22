using UnityEngine;
using System.Collections;

public class StopSignTriggerBehavior : MonoBehaviour {
	void Start() {
		// GetComponent<Renderer> ().enabled = false;
	}
	void OnTriggerExit(Collider other) {
		Messenger.Broadcast (GameEvent.RAN_STOP_SIGN);
	}
}
