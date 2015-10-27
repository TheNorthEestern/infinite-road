using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CSupport;

public class RoadSegmentBehavior : MonoBehaviour {
	void Start() {
		GameObject.Find ("Main Camera").GetComponent<Camera>().backgroundColor = Color.gray;
		GameObject[] lo = GameObject.FindGameObjectsWithTag("Terrain");
		foreach (GameObject go in lo) {
			go.GetComponent<Renderer>().material.color = new Color(ColorSupport.Downvert(133.0f), 
			                                                       ColorSupport.Downvert (66.0f),
			                                                       0f);
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