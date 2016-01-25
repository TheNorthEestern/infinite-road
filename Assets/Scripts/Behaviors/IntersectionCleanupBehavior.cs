using UnityEngine;
using System;
using System.Collections;

public class IntersectionCleanupBehavior : MonoBehaviour {
	private bool _wasDiscarded = false;
	private int counter = 0;

	void OnEnable() {
		counter = 0;
	}

	void LateUpdate() {
		RaycastHit hit;
		Vector3 segmentCenter = transform.GetChild(0).transform.position;
		Collider[]  hitColliders = Physics.OverlapSphere(segmentCenter, 25.0f);
	
		if (Array.Exists(hitColliders, element => element.CompareTag("Player"))) { counter++; }

		else if (!Array.Exists(hitColliders, element => element.CompareTag("Player")) 
			     && counter > 0){
			Debug.LogError("HILO");
			// Invoke("Discard", 1f);
			IntersectionCleanupBehavior.Discard(this);
		}
	}

	private static void Discard(IntersectionCleanupBehavior roadSegment) {
		roadSegment.gameObject.SetActive(false);
	}

}
