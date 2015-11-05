	using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class IntersectionBehavior : RoadCleanupBehavior {

	private int arrivalCount {get; set;}
	private List<Transform> _stopSignTriggers;

	void Start() {
		arrivalCount = 0;
		_stopSignTriggers = new List<Transform>();
		foreach (Transform child in transform) {
			if (child.name == "StopSignTriggers") {
				foreach (Transform stopSign in child) {
					_stopSignTriggers.Add (stopSign);
				}
			}

		}
	}

}
