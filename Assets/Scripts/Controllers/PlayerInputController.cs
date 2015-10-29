using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class PlayerInputController : MonoBehaviour {
	private AudioSource _audioSource;
	private Rigidbody _rb;
	private bool _sonicBoom = false;
	public float speed = 10.0f;
	public float gravity = -9.8f;
	public float rotationalSpeed = 1.0f;
	public float travelDirection;

	void OnCollisionEnter(Collision other) {
		Debug.Log (other.gameObject.name);
		if (other.gameObject.name == "npc") {
			Application.LoadLevel ("hillside_scene");
		}
	}

	void Start () 
	{
		_audioSource = GetComponent<AudioSource> ();
		_rb = GetComponent<Rigidbody> ();
		_rb.freezeRotation = true;
	}

	void FixedUpdate() 
	{


		float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");
		Vector3 movement;
		Vector3 restrictor = transform.position;
		restrictor.z = Mathf.Clamp (restrictor.z, -5.5f, -3.5f);


		movement = new Vector3 (moveVertical, 0.0f, -moveHorizontal);

		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			if ( Input.touchCount >= 0 ) {
				switch(Input.touchCount) {
					case 0:
						movement = Vector3.zero;
						moveVertical = 0.0f;
						break;

					case 1:
						movement = Vector3.right;
						moveVertical = 1.0f;
						break;
					
					default:
						break;
				}
			}
		}

		if (moveVertical < 0) {
			_rb.drag += .5f;
		} else if (moveVertical == 0) {
			// _rb.drag += .002f;
		} else {
			_rb.drag = 0;
		}

		if (_rb.velocity.x >= 70.0f && _sonicBoom == false) {
			_audioSource.PlayOneShot (_audioSource.clip);
			_sonicBoom = true;
		} else if (_rb.velocity.x <= 70.0f) {
			_sonicBoom = false;
		}

		transform.position = restrictor;
		_rb.velocity = Vector3.ClampMagnitude (_rb.velocity, 100.0f);
		_rb.AddForce (movement * speed);
	}
}
