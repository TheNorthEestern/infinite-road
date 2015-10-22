using UnityEngine;
using System.Collections;

public class NpcBehavior : MonoBehaviour {
	private CharacterController _cc;
	private int randomSpeed;
	private float gravity = -9.8f;

	void Start () {
		_cc = GetComponent<CharacterController> ();

	}

	void Update () {
		randomSpeed = Random.Range (1, 5);
		Vector3 movement = new Vector3 (0, randomSpeed, 0);
		movement = Vector3.ClampMagnitude (movement, randomSpeed);

		movement.x = gravity;
		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_cc.Move (movement);
	}

	void OnBecameInvisible() {
		Destroy (this.gameObject);
	}
}
