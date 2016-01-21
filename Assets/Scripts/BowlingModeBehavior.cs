/*using UnityEngine;
using System.Collections;

public class BowlingModeBehavior : MonoBehaviour {
	private bool isInBowlingMode;
	private Rigidbody _rb;

	private void ActivateBowlingMode() {
		isInBowlingMode = true;
		Debug.Log (isInBowlingMode);
		if (isInBowlingMode) {
			StartCoroutine(SetBowlingPhysics());
		}
	}

	private IEnumerator SetBowlingPhysics() {
		float origRadius = GetComponent<CapsuleCollider>().radius;
		_rb.freezeRotation = false;
		_rb.useGravity = true;
		GetComponent<CapsuleCollider>().radius = 0.060f;

		yield return new WaitForSeconds(5);

		_rb.freezeRotation = true;
		_rb.useGravity = false;
		GetComponent<CapsuleCollider>().radius = origRadius;
		isInBowlingMode = false;

		transform.position = new Vector3(transform.position.x, .36f, transform.position.z);
		transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, originalYRotation, transform.position.z));
	}
}*/
