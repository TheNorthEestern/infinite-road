	using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class IntersectionBehavior : RoadCleanupBehavior {

	private int arrivalCount {get; set;}
	private List<Transform> _stopSignTriggers;

	void Start() {
		arrivalCount = 0;
		Messenger.AddListener (GameEvent.STOP_SIGN_ARRIVAL, IncrementArrivalCount);
		_stopSignTriggers = new List<Transform>();
		foreach (Transform child in transform) {
			if (child.name == "StopSignTriggers") {
				foreach (Transform stopSign in child) {
					_stopSignTriggers.Add (stopSign);
				}
			}

		}
	}

	void OnDestroy() {
		Messenger.RemoveListener(GameEvent.STOP_SIGN_ARRIVAL, IncrementArrivalCount);
	}

	private void IncrementArrivalCount() {
		arrivalCount += 1;
		Messenger<int>.Broadcast(GameEvent.NPC_SAW_OTHER_NPC, arrivalCount, MessengerMode.DONT_REQUIRE_LISTENER);
	
	}

	private int GetArrivalCount(int arc) {
		return arrivalCount;
	}
	
}
