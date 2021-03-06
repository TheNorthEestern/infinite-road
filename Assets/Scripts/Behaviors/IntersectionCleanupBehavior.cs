﻿using UnityEngine;
using System;
using System.Collections;

public class IntersectionCleanupBehavior : MonoBehaviour {

	private int counter = 0;

	void OnEnable() {
		counter = 0;
	}

	void LateUpdate() {
		Vector3 segmentCenter = transform.GetChild(0).transform.position;
		Collider[]  hitColliders = Physics.OverlapSphere(segmentCenter, 25.0f);
	
		if (Array.Exists(hitColliders, element => element.CompareTag("Player"))) { counter++; }

		else if (!Array.Exists(hitColliders, element => element.CompareTag("Player")) 
			     && counter > 0){
			IntersectionCleanupBehavior.Discard(this);
		}
	}

	private static void Discard(IntersectionCleanupBehavior roadSegment) {
		roadSegment.gameObject.SetActive(false);
	}

}
