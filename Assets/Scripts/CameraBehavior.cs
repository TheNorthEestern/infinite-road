using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {
	public GameObject focalPoint;
	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = focalPoint.transform.position + offset;
	}
}
