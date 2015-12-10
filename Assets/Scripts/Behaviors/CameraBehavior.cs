using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	public GameObject focalPoint;
	private bool _levelStarted = false;
	private MotionBlur _blurComponent;
	private Grayscale _grayscaleComponent;
	private Vector3 offset;

	void Awake() {
		Messenger.AddListener(GameEvent.PLAYER_INITIATED_GAME, DisableSepia);
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.PLAYER_INITIATED_GAME, DisableSepia);
	}

	private void DisableSepia() {
		_grayscaleComponent.enabled = false;
	}

	void OnLevelWasLoaded(int level) {
		_levelStarted = true;
	}

	void Start () {
		offset = transform.position;
		_grayscaleComponent = GetComponent<Grayscale>();
		_blurComponent = GetComponent<MotionBlur> ();
		_blurComponent.enabled = false;
		_grayscaleComponent.enabled = true;
	}

	private IEnumerator Pause() {
		yield return new WaitForSeconds(2);
	}

	void Update () {
		if (_levelStarted) {
			transform.localRotation = Quaternion.Slerp (Quaternion.Euler (0, 45f,45f),Quaternion.Euler (0, 0, 0),  Time.time * 0.4f);
		}
		CheckAndUpdateMotionBlur ();
		transform.position = focalPoint.transform.position + offset;
	}

	private void CheckAndUpdateMotionBlur() 
	{
		if (focalPoint.GetComponent<Rigidbody> ().velocity.x >= 70) {
			_blurComponent.enabled = true;
		} else {
			_blurComponent.enabled = false;
		}
	}
}