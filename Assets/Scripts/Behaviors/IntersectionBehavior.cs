using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntersectionBehavior : MonoBehaviour {

	private List<Transform> _stopSignTriggers;

	void Start() {
		_stopSignTriggers = new List<Transform>();
		foreach (Transform child in transform) {
			if ( child.name == "StopSignTriggers" ) {
				foreach (Transform stopSign in child ) {
					// Debug.Log (stopSign);
					_stopSignTriggers.Add (stopSign);
				}
			}

		}
	}

	void Update() {
		// Debug.Log (_stopSignTriggers[0]);
	}

}
