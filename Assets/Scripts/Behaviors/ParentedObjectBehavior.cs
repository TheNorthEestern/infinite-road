using UnityEngine;
using System.Collections;

public class ParentedObjectBehavior : MonoBehaviour {

	private Renderer _parentObjectRenderer;
	private int _timesSeenByCamera = 0;

	void Start() {
		_parentObjectRenderer = GetComponent<Renderer>();
	}

	void OnBecameInvisible() {
		Destroy (this.gameObject);
	}

}
