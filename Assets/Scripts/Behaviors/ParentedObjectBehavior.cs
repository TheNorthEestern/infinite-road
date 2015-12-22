using UnityEngine;
using System.Collections;

public class ParentedObjectBehavior : MonoBehaviour {

	// protected Vector3 origPos;
	protected Quaternion origRot;

	protected void CheckIfOnRoad() {
		if (transform.position.y < 0) {
			Invoke ("Destroy", 1f);
		}
	}

	private void Destroy() {
		// origPos = transform.parent.transform.position;
		gameObject.transform.parent.gameObject.SetActive(false);
	}

	private void OnEnable() {
		// Since this object is contained in an empty game object
		// to obtain the correct position when re-enabling it
		// we have to get the position of the parent 
		transform.position = transform.parent.transform.position;
	}

	private void OnDisable() {
		CancelInvoke();
	}
}
