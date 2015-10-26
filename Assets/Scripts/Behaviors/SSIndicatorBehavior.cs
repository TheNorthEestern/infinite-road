using UnityEngine;
using System.Collections;

public class SSIndicatorBehavior : MonoBehaviour {
	
	void Start () {
		gameObject.SetActive(false);
		Messenger.AddListener (GameEvent.APPROACHING_STOP_SIGN, Activate);
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
