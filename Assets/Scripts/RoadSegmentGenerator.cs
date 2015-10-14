using UnityEngine;
using System.Collections;

public class RoadSegmentGenerator : MonoBehaviour {
	private GameObject _initialRoadSegmentInstance;
	private GameObject _roadSegmentPrefab;
	private GameObject _intersectionPrefab;
	private Vector3 _roadSegmentInstantiationPosition;
	private Vector3 _intersectionInstantiationPosition;
	private float _originalInstantiationSize;

	void Start () {
		// Load prefabs for various road segments
		_intersectionPrefab = Resources.Load ("Prefabs/Intersection", typeof(GameObject)) as GameObject;
		_roadSegmentPrefab = Resources.Load ("Prefabs/RoadSegment", typeof(GameObject)) as GameObject;

		_initialRoadSegmentInstance = Instantiate (_roadSegmentPrefab);
		_roadSegmentInstantiationPosition = _initialRoadSegmentInstance.transform.position;
		_originalInstantiationSize = GameObject.FindGameObjectWithTag ("OriginalRoadSegment").GetComponent<Renderer>().bounds.size.x;
	}

	public void Generate() {
		// Create the a new road segment at the end of the previous road segment
		int segmentChoice = Random.Range (1, 10);
		if (segmentChoice % 2 == 0) {
			_roadSegmentInstantiationPosition.x += _originalInstantiationSize;
			Instantiate (_roadSegmentPrefab, _roadSegmentInstantiationPosition, Quaternion.identity);
		} else {
			Instantiate (_intersectionPrefab, _roadSegmentInstantiationPosition, Quaternion.identity);
			_roadSegmentInstantiationPosition.x += _originalInstantiationSize + 4;
		}
	}
}
