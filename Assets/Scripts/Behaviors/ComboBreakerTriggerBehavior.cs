using UnityEngine;
using System.Collections;

public class ComboBreakerTriggerBehavior : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			Messenger<bool>.Broadcast(GameEvent.COIN_EVENT, true);
		}	
	}

	void Start() {
		GetComponent<Renderer>().enabled = false;
	}
	
}
