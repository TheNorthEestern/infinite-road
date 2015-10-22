using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	private CharacterController _characterController;
	private AudioSource _audioSource;
	private Rigidbody _rb;
	private bool _sonicBoom = false;
	public float speed = 10.0f;
	public float gravity = -9.8f;
	public float rotationalSpeed = 1.0f;
	public float travelDirection;

	private bool autoPilot = false;

	void Start () 
	{
		_audioSource = GetComponent<AudioSource> ();
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
			if ( Input.touchCount >= 0 ) {
				switch(Input.touchCount) {
					case 0:
						movement = Vector3.zero;
						moveVertical = -1.0f;
						break;

					case 1:
						movement = Vector3.right;
						break;
					
					default:
						break;
				}
			}
		}

		if (moveVertical < 0) {
			_rb.drag += .7f;
		} else {
			_rb.drag = 0;
		}

		Debug.Log ("Sonic Boom " + _sonicBoom); 
		if (_rb.velocity.x >= 70.0f && _sonicBoom == false) {
			_audioSource.PlayOneShot (_audioSource.clip);
			_sonicBoom = true;
		} else if (_rb.velocity.x <= 70.0f) {
			_sonicBoom = false;
		}

		_rb.velocity = Vector3.ClampMagnitude (_rb.velocity, 100.0f);
		_rb.AddForce (movement * speed);
	}
}
