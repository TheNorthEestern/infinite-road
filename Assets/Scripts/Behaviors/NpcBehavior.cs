using UnityEngine;
using System.Collections;

public class NpcBehavior : MonoBehaviour {
	private CharacterController _cc;
	private int randomSpeed;
	private float gravity = -9.8f;
	private Vector3 movement;
	private bool encounteredStopSign = false;

	void Start () {
		_cc = GetComponent<CharacterController> ();
		randomSpeed = Random.Range (1, 5);

	}

	void Update () {
		movement = (!encounteredStopSign) ? new Vector3 (0, randomSpeed, 0) : Vector3.zero;
		movement = Vector3.ClampMagnitude (movement, randomSpeed);
		movement.x = gravity;
		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_cc.Move (movement);
	}

	void OnBecameInvisible() {
		Destroy (this.gameObject);
	}

	private void EncounteredStopSign() {
		encounteredStopSign = !encounteredStopSign;
	}
}
