using UnityEngine;
using System.Collections;

public class NpcSemiBehavior : ParentedObjectBehavior {
	private CharacterController _cc;
	private int randomSpeed;
	private float gravity = -9.8f;
	private Vector3 movement;

	void Start () {
		_cc = GetComponent<CharacterController> ();
		randomSpeed = Random.Range (5, 10);
		movement = new Vector3(randomSpeed, gravity, 0);
	}
	
	void FixedUpdate () {
		CheckIfOnRoad();
		movement = new Vector3 (randomSpeed, gravity, 0);

		movement = Vector3.ClampMagnitude (movement, randomSpeed);
		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_cc.Move (movement);

	}

}
