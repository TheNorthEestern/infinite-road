using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class SSIndicatorBehavior : MonoBehaviour {

	void Awake () {
		GetComponent<Image>().enabled = false;
		Messenger.AddListener (GameEvent.APPROACHING_STOP_SIGN, Activate);
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.APPROACHING_STOP_SIGN, Activate);
	}

	private void Activate() {
		GetComponent<Image>().enabled = true;
		StartCoroutine(Deactivate());
	}

	private IEnumerator Deactivate() {
		yield return new WaitForSeconds(2);
		GetComponent<Image>().enabled = false;
	}

}
