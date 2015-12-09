using UnityEngine;
using System.Collections;

public class RoadSegmentGeneratorTriggerBehavior : MonoBehaviour {
	public GameObject mainController;
	void OnTriggerEnter(Collider other) {
		// Upon colliding with a player GameObject, this trigger emits
		// a 'Generate' message to it's main controller.
		if ( other.gameObject.tag != "NPC" ) {
			mainController.SendMessage ("Generate");
		}
	}

	void Start () {
		mainController = GameObject.Find ("SceneController");
		GetComponent<Renderer> ().enabled = false;
	}

}
