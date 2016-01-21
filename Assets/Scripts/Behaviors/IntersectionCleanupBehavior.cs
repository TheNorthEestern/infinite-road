using UnityEngine;
using System;
using System.Collections;

public class IntersectionCleanupBehavior : MonoBehaviour {
	private bool _playerEnteredVicinity = true;
	private int counter;

	void Update() {
		RaycastHit hit;
		Vector3 segmentCenter = transform.GetChild(0).transform.position;
		Collider[]  hitColliders = Physics.OverlapSphere(segmentCenter, 25.0f);

		if (Array.Exists(hitColliders, element => element.CompareTag("Player"))) {
			// Debug.LogError("SEEING");
			print("Player! " + transform.name + " " + transform.GetInstanceID() + " " + Time.realtimeSinceStartup);
			counter++;
		} 

		else if (!Array.Exists(hitColliders, element => CompareTag("Player")) && counter > 0){
			Invoke("Discard", 1f);
		}
	}


	private void Discard() {
		gameObject.SetActive(false);
	}

}
