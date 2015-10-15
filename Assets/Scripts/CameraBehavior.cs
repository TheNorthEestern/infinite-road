using UnityEngine;
using System;
using System.Collections;

public class CameraBehavior : MonoBehaviour {
	public GameObject focalPoint;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		TimeSpan timespan = DateTime.Now.TimeOfDay;
		DateTime time = DateTime.Now;
		// transform.position.Set (0, 0, 0);
		// Trippy camera effect
		// transform.localRotation = Quaternion.Euler (0f, (float)Math.Cos (timespan.TotalSeconds)*25f, 0f);
		transform.position = focalPoint.transform.position + offset;
	}
}