using UnityEngine;
using System.Collections;

public class StopSignTriggerBehavior : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		Messenger.Broadcast (GameEvent.RAN_STOP_SIGN);
	}
}
