using UnityEngine;
using System.Collections;

public class StopSignTriggerBehavior : MonoBehaviour {

	void Start() {
		GetComponent<Renderer> ().enabled = false;
	}

	void OnTriggerExit(Collider collidingObject) {
		collidingObject.SendMessage("EncounteredStopSign", SendMessageOptions.DontRequireReceiver);
		if (collidingObject.CompareTag("Player")) {
			Messenger.Broadcast (GameEvent.RAN_STOP_SIGN);
		}
	}

}
