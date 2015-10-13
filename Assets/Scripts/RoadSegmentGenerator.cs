using UnityEngine;
using System.Collections;

public class RoadSegmentGenerator : MonoBehaviour {
	public GameObject roadSegment;
	private Vector3 _originalInstantiationPosition;
	private float _originalInstantiationSize;
	// Use this for initialization
	void Start () {
		_originalInstantiationPosition = roadSegment.transform.position;
		_originalInstantiationSize = GameObject.FindGameObjectWithTag ("OriginalRoadSegment").GetComponent<Renderer>().bounds.size.x;
	}

	public void Generate() {
		_originalInstantiationPosition.x += _originalInstantiationSize;
		Instantiate(roadSegment, _originalInstantiationPosition, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
