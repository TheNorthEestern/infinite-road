using UnityEngine;
using System.Collections;

public class NpcBehavior : ParentedObjectBehavior {
	private CharacterController _cc;
	private int randomSpeed;
	private int arrivalNumber;
	private float gravity = -9.8f;
	private Vector3 movement;
	private bool encounteredStopSign = false;

	void Start () {
		_cc = GetComponent<CharacterController> ();
		arrivalNumber = 0;
		randomSpeed = Random.Range (5, 10);
	}

	void FixedUpdate () {
		CheckIfOnRoad();
		movement = (!encounteredStopSign) ? new Vector3 (gravity, randomSpeed, 0) : 
										    Vector3.zero;
		movement = Vector3.ClampMagnitude (movement, randomSpeed);
		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_cc.Move (movement);
	}

	private void SetArrivalNumber(int reportedArrivalNumber) {
		arrivalNumber = reportedArrivalNumber;
		StartCoroutine(ShouldProceedFromStopSign());
	}

	private void EncounteredStopSign() {
		encounteredStopSign = !encounteredStopSign;
	}

	private IEnumerator ShouldProceedFromStopSign() {
		yield return new WaitForSeconds(arrivalNumber);
		encounteredStopSign = !encounteredStopSign;
	}
}
