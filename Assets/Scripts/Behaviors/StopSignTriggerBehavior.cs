using UnityEngine;
using System.Collections;

public class StopSignTriggerBehavior : MonoBehaviour {

	void Start() {
		GetComponent<Renderer> ().enabled = false;
	}

	void OnTriggerEnter(Collider collidingObject) {
		Debug.Log (collidingObject.gameObject.name);
		Messenger.Broadcast(GameEvent.STOP_SIGN_ARRIVAL);
		collidingObject.SendMessage("EncounteredStopSign", SendMessageOptions.DontRequireReceiver);
		if (collidingObject.gameObject.tag != "NPC") {
			Messenger.Broadcast (GameEvent.RAN_STOP_SIGN);
		}
	}

	void OnTriggerExit(Collider collidingObject) {
	
	}

}
