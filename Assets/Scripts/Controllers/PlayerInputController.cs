using UnityEngine;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	private CharacterController _characterController;
	private Rigidbody _rb;
	public float speed = 10.0f;
	public float gravity = -9.8f;
	public float rotationalSpeed = 1.0f;
	public float travelDirection;

	private bool autoPilot = false;

	void Start () 
	{
		_rb = GetComponent<Rigidbody> ();
		_rb.freezeRotation = true;
		_characterController = GetComponent<CharacterController> ();
	}

	void FixedUpdate() 
	{
		float moveVertical = Input.GetAxis ("Vertical");
		
		Vector3 movement;

		movement = new Vector3 (moveVertical, 0.0f, 0.0f);

		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			if ( Input.touchCount > 0 ) {

				movement = Vector3.right;

				switch(Input.touchCount) {
				case 1:
					speed = 10;
					break;
					
				case 2:
					speed = -10;
					break;
					
				default:
					break;
				}
			} 
			if ( Input.touchCount == 0 ) {
				movement = Vector3.zero;
			}

		}

		if (moveVertical < 0) {
			_rb.drag += .7f;
		} else {
			_rb.drag = 0;
		}

		_rb.velocity = Vector3.ClampMagnitude (_rb.velocity, 100.0f);
		_rb.AddForce (movement * speed);
	}
}
