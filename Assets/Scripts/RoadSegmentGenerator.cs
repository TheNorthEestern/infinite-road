using UnityEngine;
using System.Collections;

public class RoadSegmentGenerator : MonoBehaviour {
	private GameObject _initialRoadSegmentInstance;
	private GameObject _roadSegmentPrefab;
	private GameObject _intersectionSegment;
	private Vector3 _roadSegmentInstantiationPosition;
	private Vector3 _intersectionInstantiationPosition;
	private float _originalInstantiationSize;

	void Start () {
		_roadSegmentPrefab = Resources.Load ("Prefabs/RoadSegment", typeof(GameObject)) as GameObject;
		_initialRoadSegmentInstance = Instantiate (_roadSegmentPrefab);
		_roadSegmentInstantiationPosition = _initialRoadSegmentInstance.transform.position;
		_originalInstantiationSize = GameObject.FindGameObjectWithTag ("OriginalRoadSegment").GetComponent<Renderer>().bounds.size.x;
	}

	public void Generate() {
		_roadSegmentInstantiationPosition.x += _originalInstantiationSize;
		Instantiate (_roadSegmentPrefab, _roadSegmentInstantiationPosition, Quaternion.identity);
	}
}
