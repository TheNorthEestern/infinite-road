using UnityEngine;
using System.Collections;

public class NpcBehavior : MonoBehaviour {
	private CharacterController _cc;
	private int randomSpeed;
	private int arrivalNumber;
	private float gravity = -9.8f;
	private Vector3 movement;
	private bool encounteredStopSign = false;

	void Start () {
		Messenger<int>.AddListener(GameEvent.NPC_SAW_OTHER_NPC, SetArrivalNumber);
		_cc = GetComponent<CharacterController> ();
		arrivalNumber = 0;
		randomSpeed = Random.Range (5, 10);
	}

	void Update () {
		movement = (!encounteredStopSign) ? new Vector3 (gravity, randomSpeed, 0) : 
										    Vector3.zero;
		movement = Vector3.ClampMagnitude (movement, randomSpeed);
		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_cc.Move (movement);
	}

	void OnBecameInvisible() {
		Invoke ("Discard", 5);
	}

	private void Discard() {
		Destroy (this.gameObject);
	}

	private void SetArrivalNumber(int reportedArrivalNumber) {
		arrivalNumber = reportedArrivalNumber;
		Debug.Log ("My Arrival # " + arrivalNumber);
	}

	private void EncounteredStopSign() {
		encounteredStopSign = !encounteredStopSign;
	}

	private IEnumerator ShouldProceedFromStopSign() {
		yield return new WaitForSeconds(0);
	}
}
