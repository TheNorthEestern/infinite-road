using UnityEngine;
using System.Collections;

public class RoadSegmentGenerator : MonoBehaviour {
	private GameObject _roadSegment;
	public GameObject intersection;
	private Vector3 _roadSegmentInstantiationPosition;
	private Vector3 _intersectionInstantiationPosition;
	private float _originalInstantiationSize;
	// Use this for initialization

	void Start () {
		_roadSegment = Instantiate (Resources.Load ("Prefabs/RoadSegment", typeof(GameObject))) as GameObject;
		_intersectionInstantiationPosition = intersection.transform.position;
		_roadSegmentInstantiationPosition = _roadSegment.transform.position;
		_originalInstantiationSize = GameObject.FindGameObjectWithTag ("OriginalRoadSegment").GetComponent<Renderer>().bounds.size.x;
	}
	// 15.81, -16.4, -25.5
	public void Generate() {
		_roadSegmentInstantiationPosition.x += _originalInstantiationSize;
		Instantiate (_roadSegment, _roadSegmentInstantiationPosition, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
