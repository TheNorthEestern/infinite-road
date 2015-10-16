using UnityEngine;
using System;
using System.Collections;

public class DirectionalLightBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		TimeSpan timespan = DateTime.Now.TimeOfDay;
		transform.localRotation = Quaternion.Euler (0f, (float)Math.Cos (timespan.TotalSeconds)*325f, 0f);
	}
}
