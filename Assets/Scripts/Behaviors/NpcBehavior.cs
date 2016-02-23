using UnityEngine;
using System.Collections;

public class NpcBehavior : ParentedObjectBehavior {
	private CharacterController _cc;
	private int randomStartSpeed;
	private int cruisingSpeed;
	private int arrivalNumber;
	private float gravity = -9.8f;
	private Vector3 movement;
	private bool encounteredStopSign = false;
	private bool crashed = false;

	void Start () {
		_cc = GetComponent<CharacterController> ();
		arrivalNumber = 0;
		randomStartSpeed = Random.Range (5, 10);
		cruisingSpeed = randomStartSpeed;
	}

	void OnDrawGizmos() {
		Gizmos.DrawLine(transform.position, transform.forward);
	}

	void ScanAhead() {
		// Only track objects in the 'UI' layer
		LayerMask mask = 5;
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100)) {
			if (hit.collider.CompareTag("Player")) {
				// Debug.LogError("STOP  " + transform.name);
				cruisingSpeed = 0;
			} else {
				cruisingSpeed = randomStartSpeed;
			} 
		}
	}

	void FixedUpdate () {
		CheckIfOnRoad();
		ScanAhead();
		movement = new Vector3(0, cruisingSpeed, gravity);
		movement = Vector3.ClampMagnitude (movement, randomStartSpeed);
		movement *= Time.deltaTime;
		movement = transform.TransformDirection (-movement);
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

	private void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("Player")) {
			
		}	
	}
}
