using UnityEngine;
using System.Collections;

public class RoadSegmentGenerator : MonoBehaviour {
	private GameObject _roadSegment;
	private GameObject _intersectionSegment;
	private Vector3 _roadSegmentInstantiationPosition;
	private Vector3 _intersectionInstantiationPosition;
	private float _originalInstantiationSize;

	void Start () {
		_roadSegment = Instantiate (Resources.Load ("Prefabs/RoadSegment", typeof(GameObject))) as GameObject;
		_roadSegmentInstantiationPosition = _roadSegment.transform.position;
		_originalInstantiationSize = GameObject.FindGameObjectWithTag ("OriginalRoadSegment").GetComponent<Renderer>().bounds.size.x;
	}

	public void Generate() {
		_roadSegmentInstantiationPosition.x += _originalInstantiationSize;
		Instantiate (_roadSegment, _roadSegmentInstantiationPosition, Quaternion.identity);
	}
}
