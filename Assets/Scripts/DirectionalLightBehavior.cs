using UnityEngine;
using System;
using System.Collections;

public class DirectionalLightBehavior : MonoBehaviour {
	public byte red, green, blue;
	public Color lightColor;
	// Use this for initialization
	void Start () {
		red = 60; 
		green = 60; 
		blue = 60;
		lightColor = new Color (red, green, blue);
		GetComponent<Light> ().color = lightColor;
	}
	
	// Update is called once per frame
	void Update () {
		TimeSpan timespan = DateTime.Now.TimeOfDay;
		transform.localRotation = Quaternion.Euler (0f, (float)Math.Cos (timespan.TotalSeconds)*325f, 0f);
	}
}
