﻿using UnityEngine;
using System.Collections;

public class StopSignTriggerBehavior : MonoBehaviour {

	void Start() {
		GetComponent<Renderer> ().enabled = false;
	}

	void OnTriggerEnter(Collider collidingObject) {
		Messenger.Broadcast(GameEvent.STOP_SIGN_ARRIVAL);
		collidingObject.SendMessage("EncounteredStopSign", SendMessageOptions.DontRequireReceiver);
		if (collidingObject.name != "npc") {
			Messenger.Broadcast (GameEvent.RAN_STOP_SIGN);
		}
	}

	void OnTriggerExit(Collider collidingObject) {
	
	}

}
