using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RoadSegmentBehavior : MonoBehaviour {

	private Color32 winter;
	private Color32 spring;
	private Color32 summer;
	private Color32 autumn;

	void Start() {
		winter = new Color32(246, 243, 237, 255);
		spring = new Color32(133, 66, 0, 255);
		summer = new Color32(137, 182, 65, 255);
		autumn = new Color32(246, 107, 0, 255);
		int currentSecond = DateTime.Now.Second;

		GameObject.Find ("Main Camera").GetComponent<Camera>().backgroundColor = Color.gray;
		GameObject[] lo = GameObject.FindGameObjectsWithTag("Terrain");
		foreach (GameObject go in lo) {
			if (currentSecond < 15) {
				go.GetComponent<Renderer>().material.color = winter;
			} 
			else if (currentSecond < 30) {
				go.GetComponent<Renderer>().material.color = spring;
			}
			else if (currentSecond < 45) {
				go.GetComponent<Renderer>().material.color = summer;
			} 
			else {
				go.GetComponent<Renderer>().material.color = autumn;
			}
			// go.GetComponent<Renderer>().material.color = summer;
		}
	}

	// OnBecameVisible is called when the object enters the camera's
	// view frustum
	void OnBecameVisible () {

	}

	// OnBecameInvisible is called when the object is out of the camera's
	// view frustum
	void OnBecameInvisible () {
		Invoke ("Discard", 5);
	}

	void Discard() {
		Destroy (this.gameObject);
	}

}