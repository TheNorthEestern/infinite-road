using UnityEngine;
using System.Collections;

public class PickUpTriggerBehavior : MonoBehaviour {

	void Start() {
		GetComponent<Renderer>().enabled = false;
	}

	void OnTriggerEnter(Collider other) {
		if ( other.CompareTag ("Player") ) {
			Debug.Log(other.transform.position.z);
			Messenger<float>.Broadcast (GameEvent.RAN_STOP_SIGN, other.transform.position.z);
			Messenger<bool>.Broadcast(GameEvent.COIN_EVENT, false);
			Discard();
		}
	}
		
	private void Discard() {
		transform.parent.gameObject.SetActive(false);
	}
}
