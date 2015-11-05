﻿using UnityEngine;
using System.Collections;

public class StopSignTriggerBehavior : MonoBehaviour {

	void Start() {
		GetComponent<Renderer> ().enabled = false;
	}

	void OnTriggerExit(Collider collidingObject) {
		collidingObject.SendMessage("EncounteredStopSign", SendMessageOptions.DontRequireReceiver);
		if (collidingObject.gameObject.tag == "Player") {
			Debug.Log (collidingObject.gameObject.name);
			Messenger.Broadcast (GameEvent.RAN_STOP_SIGN, MessengerMode.DONT_REQUIRE_LISTENER);
		}
	}

}
