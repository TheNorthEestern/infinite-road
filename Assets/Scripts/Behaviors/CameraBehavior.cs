using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	public GameObject focalPoint;
	// private Camera _camera;
	private MotionBlur _blurComponent;
	private Vector3 offset;
	private Animator _animator;

	void Start () {
		offset = transform.position;
		_blurComponent = GetComponent<MotionBlur> ();
		_blurComponent.enabled = false;
		_animator = GetComponent<Animator>();
		// _camera = GetComponent<Camera> ();
	}

	private IEnumerator Pause() {
		yield return new WaitForSeconds(2);
	}

	void Update () {
		// TimeSpan timespan = DateTime.Now.TimeOfDay;
		// DateTime time = DateTime.Now;
		// transform.position.Set (0, 0, 0);
		// Trippy camera effect
		// Debug.Log ((float)Math.Cos (timespan.TotalSeconds)*25f);
		// transform.localRotation = Quaternion.Euler (0f, (float)Math.Cos (timespan.TotalSeconds)*25f, 0f);
		// transform.localRotation = Quaternion.Euler (0f, 30.0f, 0f);

		transform.localRotation = Quaternion.Slerp (Quaternion.Euler (0, 45f,45f),Quaternion.Euler (0, 0, 0),  Time.time * 0.4f);
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