using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	private CharacterController _characterController;
	public float speed = 10.0f;
	public float gravity = -9.8f;
	public float rotationalSpeed = 1.0f;
	public float travelDirection;

	private bool autoPilot = false;

	void Start () {
		_characterController = GetComponent<CharacterController> ();
	}

	void Update () {
		Vector3 movement = new Vector3 (0, 0, 0);
	
		float deltaZ = Input.GetAxis ("Vertical") * speed;

		if (Input.GetKeyDown (KeyCode.F)) {
			autoPilot = !autoPilot;
		}

		if (autoPilot == true) {
			movement = new Vector3 (0, 0, 10 * travelDirection);
		} else {
			movement = new Vector3 (0, 0, deltaZ * travelDirection);
		}


		movement = Vector3.ClampMagnitude (movement, speed);

		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_characterController.Move (movement);
	}
}
