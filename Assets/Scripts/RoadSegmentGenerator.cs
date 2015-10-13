using UnityEngine;
using System.Collections;

public class RoadSegmentGenerator : MonoBehaviour {
	public GameObject roadSegment;
	public GameObject intersection;
	private Vector3 _roadSegmentInstantiationPosition;
	private Vector3 _intersectionInstantiationPosition;
	private float _originalInstantiationSize;
	// Use this for initialization
	void Start () {
		_intersectionInstantiationPosition = intersection.transform.position;
		_roadSegmentInstantiationPosition = roadSegment.transform.position;
		_originalInstantiationSize = GameObject.FindGameObjectWithTag ("OriginalRoadSegment").GetComponent<Renderer>().bounds.size.x;
	}
	// 15.81, -16.4, -25.5
	public void Generate() {
		int n = Random.Range (1, 10);
		if (n % 2 == 0) {
			_roadSegmentInstantiationPosition.x += _originalInstantiationSize;
			Instantiate (roadSegment, _roadSegmentInstantiationPosition, Quaternion.identity);
		} else {
			_intersectionInstantiationPosition.x += _originalInstantiationSize + 4;
			Instantiate (intersection, _intersectionInstantiationPosition, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
