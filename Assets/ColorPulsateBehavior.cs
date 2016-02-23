using UnityEngine;
using System.Collections;

public class ColorPulsateBehavior : MonoBehaviour {
	private Renderer _renderer;
	private float speed = 0.5f;

	void Start() {
		_renderer = GetComponent<Renderer>();
	}
	// Update is called once per frame
	void Update () {
		_renderer.material.color = Color.Lerp(_renderer.material.color, Color.blue, Mathf.PingPong(Time.deltaTime * 15, 1));
	}

}
