using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	protected static AudioSource _audioSource;
	protected static Rigidbody _rb;
	protected static bool _sonicBoom = false;
	protected static float speed = 10.0f;
	protected static float moveVertical;
	protected static float moveHorizontal;
	protected const float GRAVITY = -9.8f;
	protected static Vector3 movement;
	protected static Vector3 restrictor;
	private bool isPassing;
	[SerializeField] private float maxSpeed = 100.0f;

	void Start () 
	{
		_audioSource = GetComponent<AudioSource> ();
		_rb = GetComponent<Rigidbody> ();
		_rb.freezeRotation = true;

		// _rb.freezeRotation = false;
		// _rb.useGravity = true;
		// GetComponent<CapsuleCollider>().radius = 0.060f;
	}

	private void Update() {
		// Vector3.back
		Debug.DrawRay (transform.position, Vector3.back * 10, Color.red);
	}

	private void RestrictPlayerMovement() {
		restrictor = transform.position;
		restrictor.z = Mathf.Clamp (restrictor.z, -5.5f, -3.3f);
		_rb.transform.position = restrictor;
	}

	private void ApplyRigidbodyMechanics() {
		if (moveVertical < 0) {
			_rb.drag += .5f;
		} else if (moveVertical == 0) {
			_rb.drag += .002f;
		} else {
			_rb.drag = 0;
		}
	}

	private void PlayerPivotMechanics() {
		if ( moveHorizontal > 0 ) {
			transform.localRotation = Quaternion.Euler (transform.localRotation.x, 100, transform.localRotation.z);
		} else if ( moveHorizontal < 0 ){
			transform.localRotation = Quaternion.Euler (transform.localRotation.x, 80, transform.localRotation.z);
		} else if ( moveHorizontal == 0 ) {
			transform.localRotation = Quaternion.Euler (transform.localRotation.x, 90, transform.localRotation.z);
		}
	}

	private void MonitorPlayerSpeed() {
		if (_rb.velocity.x >= 70.0f && _sonicBoom == false) {
			_audioSource.PlayOneShot (_audioSource.clip);
			_sonicBoom = true;
		} else if (_rb.velocity.x <= 70.0f) {
			_sonicBoom = false;
		}
	}

	private void CheckIfPlayerPassing() {
		Ray passingRay = new Ray(transform.position, Vector3.back);
		RaycastHit hit;
		if (Physics.SphereCast(passingRay, 0.75f, out hit)) {
			isPassing = true;
		} else{
			isPassing = false;
		}
	}

	public virtual void FixedUpdate() 
	{
		RestrictPlayerMovement();
		ApplyRigidbodyMechanics();
		PlayerPivotMechanics();	
		MonitorPlayerSpeed();
		CheckIfPlayerPassing();

		_rb.velocity = Vector3.ClampMagnitude (_rb.velocity, maxSpeed);
		_rb.AddForce (movement * speed);
	}

	void OnCollisionEnter(Collision other) {
		Debug.Log (other.gameObject.name);
		if (other.gameObject.tag == "NPC") {
			Application.LoadLevel ("hillside_scene");
		}
	}

}
