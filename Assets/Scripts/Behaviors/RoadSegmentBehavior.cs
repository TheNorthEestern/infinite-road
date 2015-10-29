using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CSupport;

public class RoadSegmentBehavior : MonoBehaviour {
	void Start() {
		// 246	243	237 - snow white
		// 133, 66, 0 - mountain brown
		GameObject.Find ("Main Camera").GetComponent<Camera>().backgroundColor = Color.gray;
		GameObject[] lo = GameObject.FindGameObjectsWithTag("Terrain");
		foreach (GameObject go in lo) {
			go.GetComponent<Renderer>().material.color = new Color(ColorSupport.Downvert(246.0f), 
			                                                       ColorSupport.Downvert (243.0f),
			                                                       ColorSupport.Downvert (237.0f));
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