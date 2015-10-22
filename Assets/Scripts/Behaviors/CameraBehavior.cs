using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	public GameObject focalPoint;
	// private Camera _camera;
	private MotionBlur _blurComponent;
	private Vector3 offset;

	void Start () {
		offset = transform.position;
		_blurComponent = GetComponent<MotionBlur> ();
		_blurComponent.enabled = false;

		// _camera = GetComponent<Camera> ();
	}

	void Update () {
		// TimeSpan timespan = DateTime.Now.TimeOfDay;
		// DateTime time = DateTime.Now;
		CheckAndUpdateMotionBlur ();
		// transform.position.Set (0, 0, 0);
		// Trippy camera effect
		//  transform.localRotation = Quaternion.Euler (0f, (float)Math.Cos (timespan.TotalSeconds)*25f, 0f);
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