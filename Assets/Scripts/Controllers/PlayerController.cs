using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class PlayerController : MonoBehaviour {
	protected static Rigidbody _rb;
	protected static CharacterController _cc;
	protected static bool _sonicBoom = false;
	protected static float speed = 10.0f;
	protected static float moveVertical;
	protected static float moveHorizontal;
	protected static Vector3 movement;
	protected static Vector3 restrictor;
	protected static float leftBound = -3.3f;
	protected static float rightBound = -5.5f;
	protected float gravity = 0;
	protected bool left = false;
	protected bool right = true;
	protected bool lane;
	private GameObject _camera;
	private GameObject _laneWarning;
	private AudioSource _engineSound;
	private bool isPaused;
	private bool isInFireballMode = true;
	private float originalYPosition;
	private float originalYRotation;
	private Animator _animator;
	public Vector3 startPosition = Vector3.zero;
	public GameObject uiController;
	public bool LaneState { 
		get { return lane; } 
	}
	private float maxSpeed = 20.0f;

	void Awake() {
		Messenger<float>.AddListener(GameEvent.SPEED_SLIDER_CHANGED, OnSpeedChanged);
		Messenger.AddListener(GameEvent.PLAYER_INITIATED_GAME, LowerSpeed);
	}

	void OnDestroy() {
		Messenger<float>.RemoveListener(GameEvent.SPEED_SLIDER_CHANGED, OnSpeedChanged);
		Messenger.RemoveListener(GameEvent.PLAYER_INITIATED_GAME, LowerSpeed);
	}

	private void LowerSpeed() {
		maxSpeed = 15.0f;
		_engineSound.Play();
	}

	private void OnSpeedChanged(float newSpeed) {
		maxSpeed = newSpeed;
	}

	void Start () 
	{
		startPosition = transform.position;
		lane = right;
		uiController = GameObject.Find ("UIController");
		_camera = GameObject.Find ("Main Camera");
		_engineSound = GetComponent<AudioSource> ();
		_rb = GetComponent<Rigidbody> ();
		_animator = GetComponent<Animator>();
		_laneWarning = transform.FindChild("LaneWarning").gameObject;
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
		if (!_animator.GetBool("isInBowlingMode")) {
			float computedTurnAngle = (Convert.ToBoolean(moveHorizontal)) ? ((0.2f * moveHorizontal) * 100.0f) + 90.0f : 90.0f;
			Vector3 turnAngle = new Vector3(transform.localRotation.x, computedTurnAngle, transform.localRotation.z);
			transform.localRotation = Quaternion.Euler (turnAngle);
		}
	}

	/* private void MonitorPlayerSpeed() {
		if (_rb.velocity.x >= 70.0f && _sonicBoom == false) {
			_audioSource.PlayOneShot (_audioSource.clip);
			_sonicBoom = true;
		} else if (_rb.velocity.x <= 70.0f) {
			_sonicBoom = false;
		}
	} */

	private void CheckIfOncoming() {
		// Only track objects in the 'Default' layer
		LayerMask mask = 1;
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 100, mask.value)) {
			if (hit.collider.CompareTag("NPC") && lane == left) {
				Debug.Log(hit.collider.name + " " + hit.distance);
				_camera.GetComponent<Animator>().SetBool("NearingIntersection", true);
				_laneWarning.SetActive(true);
			} else {
				_laneWarning.SetActive(false);
				_camera.GetComponent<Animator>().SetBool("NearingIntersection", false);
			}
		}
	}

	private void CheckAndUpdateLaneSelection() {
		if ( _rb.velocity.x > 2.0f ) 
		{
			if ( moveHorizontal < 0 ) {
				_engineSound.pitch = 0.95f;
				uiController.GetComponent<UIController>().PlayScreechSound();
				lane = left;
			}
			if ( moveHorizontal > 0 ) {
				_engineSound.pitch = 1.05f;
				uiController.GetComponent<UIController>().PlayScreechSound();
				lane = right;
			}

			Vector3 heightRestrictor = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0.9f, 0.9f), transform.position.z);
			transform.position = heightRestrictor;

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

	private IEnumerator PlaySound() {
		// Emitting to UIController
		Messenger.Broadcast(GameEvent.APPROACHING_ONCOMING_TRAFFIC);
		yield return new WaitForSeconds(3);
	}

	private void ActivateBowlingMode () {
		_animator.enabled = true;
		_animator.SetBool("isInBowlingMode", true);
	}

	public void DeactivateBowlingMode() {
		_animator.SetBool("isInBowlingMode", false);
		_rb.constraints = RigidbodyConstraints.FreezeRotationX;
		_animator.enabled = false;
	}

	protected virtual void FixedUpdate() 
	{
		CheckAndUpdateLaneSelection();
		RestrictPlayerMovement();
		ApplyRigidbodyMechanics();
		PlayerPivotMechanics();	
		// MonitorPlayerSpeed();
		CheckIfOncoming();

		_rb.velocity = Vector3.ClampMagnitude (_rb.velocity, maxSpeed);
		_rb.AddForce (movement * speed);

		if (Input.GetKeyDown(KeyCode.F)) {
			ActivateBowlingMode();
		}

	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag("NPC") && _animator.GetBool("isInBowlingMode")) {
			other.gameObject.GetComponent<ParentedObjectBehavior>().Destroy();	
		}
		else if (other.gameObject.tag == "NPC") {
			_engineSound.Stop();
			PlayerPrefs.SetFloat ("CurrentScore", uiController.GetComponent<UIController>().score);
			if ( PlayerPrefs.GetFloat ("hiscore") < uiController.GetComponent<UIController>().score) {
				PlayerPrefs.SetFloat ("hiscore", uiController.GetComponent<UIController>().score);
			}
			Messenger.Broadcast (GameEvent.GAME_ENDED);
			// Application.LoadLevel ("hillside_scene");
		}
	}

}
