using UnityEngine;
using System.Collections;

public class SSIndicatorBehavior : MonoBehaviour {
	
	void Start () {
		Messenger.AddListener (GameEvent.APPROACHING_STOP_SIGN, Activate);
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.APPROACHING_STOP_SIGN, Activate);
	}

	private void Activate() {
		gameObject.SetActive (true);
		StartCoroutine(Deactivate());
	}

	private IEnumerator Deactivate() {
		yield return new WaitForSeconds(2);
		gameObject.SetActive (false);
	}

}
