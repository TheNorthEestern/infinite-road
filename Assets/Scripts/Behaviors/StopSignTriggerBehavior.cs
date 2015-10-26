using UnityEngine;
using System.Collections;

public class StopSignTriggerBehavior : MonoBehaviour {
	void Start() {
		GetComponent<Renderer> ().enabled = false;
	}

	void OnTriggerEnter(Collider collidingObject) {
		Debug.Log ("Hello");
		collidingObject.SendMessage("EncounteredStopSign");
	}

	void OnTriggerExit(Collider collidingObject) {
		if (collidingObject.name != "npc") {
			Messenger.Broadcast (GameEvent.RAN_STOP_SIGN);
		}
	}

}
