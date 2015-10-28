using UnityEngine;
using System.Collections;

public class ParentedObjectBehavior : MonoBehaviour {

	private Renderer _parentObjectRenderer;
	private int _timesSeenByCamera = 0;

	void Start() {
		_parentObjectRenderer = GetComponent<Renderer>();
	}

	void OnBecameVisible() {
		Debug.Log ("Hello");
	}

	void OnBecameInvisible() {
		Debug.Log ("I'm invisible now!");
		Destroy (this.gameObject);
	}

	void Update() {
		// Debug.Log ("Renderer State -> " + GetComponent<Renderer>().isVisible);
		// Debug.Log ("Times Seen By Camera -> " + _timesSeenByCamera);
	}

}
