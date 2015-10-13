using UnityEngine;
using System.Collections;

public class ZombieModelController : MonoBehaviour {
	private CharacterController _characterController;
	public float speed = 10.0f;
	public float gravity = -9.8f;
	public float rotationalSpeed = 1.0f;
	public float travelDirection;
	// Use this for initialization
	void Start () {
		_characterController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		// float deltaX = Input.GetAxis ("Horizontal") * speed;
		// float deltaZ = Input.GetAxis ("Vertical") * speed;
		Vector3 movement = new Vector3 (15 * travelDirection, 0, 0);
		movement = Vector3.ClampMagnitude (movement, speed);

		// movement.y = gravity;
		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_characterController.Move (movement);
	}
}
