using UnityEngine;
using System.Collections;

public class RoadSegmentGeneratorTriggerBehavior : MonoBehaviour {
	public GameObject sceneController;
	void OnTriggerEnter(Collider other) {
		// Upon colliding with a player GameObject, this trigger emits
		// a 'Generate' message to it's main controller.
		if ( other.gameObject.CompareTag("Player") ) {
			sceneController.SendMessage ("Generate");
		}
	}

	void Start () {
		sceneController = GameObject.Find ("SceneController");
		GetComponent<Renderer> ().enabled = false;
	}

}
