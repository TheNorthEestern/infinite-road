using UnityEngine;
using System.Collections;

public class RoadSegmentGeneratorTriggerBehavior : MonoBehaviour {
	public GameObject mainController;
	void OnTriggerEnter(Collider other) {
		// Upon colliding with a player GameObject, this trigger emits
		// a 'Generate' message to it's main controller.
		mainController.SendMessage ("Generate");
	}

	// Use this for initialization
	void Start () {
		// Makes the trigger cube invisible.
		GetComponent<Renderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
