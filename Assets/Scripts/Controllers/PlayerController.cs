using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {
	protected static AudioSource _audioSource;
	protected static Rigidbody _rb;
	protected static bool _sonicBoom = false;
	protected static float speed = 10.0f;
	protected static float moveVertical;
	protected static float moveHorizontal;
	protected static Vector3 movement;
	protected static Vector3 restrictor;
	protected static float leftBound = -3.3f;
	protected static float rightBound = -5.5f;
	protected const float GRAVITY = -9.8f;
	protected bool left = false;
	protected bool right = true;
	protected bool lane;
	private GameObject _camera;
	[SerializeField] private GameObject _uiController;
	private bool isPaused;
	private bool isInBowlingMode = false;
	public Vector3 startPosition = Vector3.zero;

	private float maxSpeed = 15.0f;

	void Awake() {
		Messenger<float>.AddListener(GameEvent.SPEED_SLIDER_CHANGED, OnSpeedChanged);
		Messenger.AddListener(GameEvent.PLAYER_INITIATED_GAME, LowerSpeed);
		Messenger.AddListener(GameEvent.PLAYER_GOT_TEN_PASSES, ActivateBowlingMode);
	}

	void OnDestroy() {
		Messenger<float>.RemoveListener(GameEvent.SPEED_SLIDER_CHANGED, OnSpeedChanged);
		Messenger.RemoveListener(GameEvent.PLAYER_INITIATED_GAME, LowerSpeed);
		Messenger.RemoveListener(GameEvent.PLAYER_GOT_TEN_PASSES, ActivateBowlingMode);
	}

	private void LowerSpeed() {
		maxSpeed = 15.0f;
	}

	private void OnSpeedChanged(float newSpeed) {
		maxSpeed = newSpeed;
	}

	void Start () 
	{
		startPosition = transform.position;
		lane = right;
		_camera = GameObject.Find ("Main Camera");
		_audioSource = GetComponent<AudioSource> ();
		_rb = GetComponent<Rigidbody> ();
		_rb.freezeRotation = true;
		// _rb.freezeRotation = false;
		// _rb.useGravity = true;
		// GetComponent<CapsuleCollider>().radius = 0.060f;
	}

	private void RestrictPlayerMovement() {
		restrictor = transform.position;
		restrictor.z = Mathf.Clamp (restrictor.z, rightBound, leftBound);
		transform.position = restrictor;
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
			// pivot player rightward
			transform.localRotation = Quaternion.Euler (transform.localRotation.x, 100, transform.localRotation.z);
		} else if ( moveHorizontal < 0 ){
			// pivot player leftward
			transform.localRotation = Quaternion.Euler (transform.localRotation.x, 80, transform.localRotation.z);
		} else if ( moveHorizontal == 0 ) {
			// re-center player
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

	private void CheckIfOncoming() {
		Vector3 oncomingRayVector = new Vector3(transform.position.x + 0.5f, transform.position.y + 20.0f, transform.position.z);
		Ray passingRay = new Ray(oncomingRayVector, Vector3.right);
		RaycastHit hit;
		if (Physics.SphereCast(passingRay, 1.0f, out hit, 15)) {
			if ( hit.collider.CompareTag("NPC") ) {
				_camera.GetComponent<Animator>().SetBool("NearingIntersection", true);
			}
		} else{
				_camera.GetComponent<Animator>().SetBool("NearingIntersection", false);
		}	
	}

	private void CheckAndUpdateLaneSelection() {
		if ( _rb.velocity.x > 2.0f ) 
		{
			if ( moveHorizontal < 0 ) lane = left;
			if ( moveHorizontal > 0 ) lane = right;


			if ( lane == left ) {
				if ( transform.position.z < leftBound ) { 
					Vector3 endPos = new Vector3(0, 0, -(leftBound));
					_rb.MovePosition(transform.position + endPos * Time.deltaTime * 2);
				}
			}

			if ( lane == right ) {
				if ( transform.position.z > rightBound ) {
					Vector3 endPos = new Vector3(0, 0, rightBound);
					_rb.MovePosition(transform.position + endPos * Time.deltaTime * 1);
				}
			}
	
		}
	}

	private void ActivateBowlingMode() {
		isInBowlingMode = !isInBowlingMode;
		if (isInBowlingMode) {
			StartCoroutine(SetBowlingPhysics());
		}
	}

	private IEnumerator SetBowlingPhysics() {
		float origRadius = GetComponent<CapsuleCollider>().radius;
		_rb.freezeRotation = false;
		_rb.useGravity = true;
		GetComponent<CapsuleCollider>().radius = 0.060f;
		yield return new WaitForSeconds(2);
		_rb.freezeRotation = true;
		_rb.useGravity = false;
		GetComponent<CapsuleCollider>().radius = origRadius;
	}

	private IEnumerator PlaySound() {
		// Emitting to UIController
		Messenger.Broadcast(GameEvent.APPROACHING_ONCOMING_TRAFFIC);
		yield return new WaitForSeconds(3);
	}

	public virtual void FixedUpdate() 
	{
		CheckAndUpdateLaneSelection();
		// RestrictPlayerMovement();
		ApplyRigidbodyMechanics();
		PlayerPivotMechanics();	
		MonitorPlayerSpeed();
		CheckIfOncoming();

		_rb.velocity = Vector3.ClampMagnitude (_rb.velocity, maxSpeed);
		_rb.AddForce (movement * speed);

	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "NPC") {
			// PlayerPrefs.SetFloat ("highscore", 0);
			Application.LoadLevel ("hillside_scene");
		}
	}

}
