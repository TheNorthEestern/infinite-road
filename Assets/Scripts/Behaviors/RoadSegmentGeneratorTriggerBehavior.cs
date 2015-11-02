using UnityEngine;
using System.Collections;

public class RoadSegmentGeneratorTriggerBehavior : MonoBehaviour {
	public GameObject mainController;
	void OnTriggerEnter(Collider other) {
		// Upon colliding with a player GameObject, this trigger emits
		// a 'Generate' message to it's main controller.

		if ( other.gameObject.name != "npc" ) {
			mainController.SendMessage ("Generate");
		}
	}

	void Awake() {
		mainController = GameObject.Find ("SceneController");
	}

	void Start () {
		GetComponent<Renderer> ().enabled = false;
	}

}
