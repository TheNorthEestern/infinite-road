using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class RoadSegmentGenerator : MonoBehaviour {
	private GameObject _initialRoadSegmentInstance;
	private GameObject _roadSegmentPrefab;
	private List<GameObject> _roadSegmentPrefabs;
	private GameObject _intersectionPrefab;
	private GameObject _sceneController;
	private GameObject[] spawnPoints;
	private Vector3 _roadSegmentInstantiationPosition;
	private Vector3 _intersectionInstantiationPosition;
	private float _originalInstantiationSize;
	public bool _playerStartedGame = false;

	void Awake() {
		Messenger.AddListener(GameEvent.PLAYER_INITIATED_GAME, StartNormalSegmentGeneration);
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.PLAYER_INITIATED_GAME, StartNormalSegmentGeneration);
	}

	private void StartNormalSegmentGeneration() {
		_playerStartedGame = true;
	}

	void Start () {
		// Load prefabs for various road segments
		_sceneController = GameObject.Find ("SceneController");
 		_intersectionPrefab = Resources.Load ("Prefabs/Intersection", typeof(GameObject)) as GameObject;
		_roadSegmentPrefab = Resources.Load ("Prefabs/RoadSegment", typeof(GameObject)) as GameObject;
		_initialRoadSegmentInstance = Instantiate (_roadSegmentPrefab);
		_roadSegmentInstantiationPosition = _initialRoadSegmentInstance.transform.position;
		_intersectionInstantiationPosition = _intersectionPrefab.transform.position;
		_originalInstantiationSize = GameObject.FindGameObjectWithTag ("OriginalRoadSegment").GetComponent<Renderer>().bounds.size.x;

		// Create road segment just before the initial one the player is on to
		// create the illusion of a truly infinite road
		Instantiate (_roadSegmentPrefab, 
		             new Vector3(_roadSegmentInstantiationPosition.x - _originalInstantiationSize, 
		            			 _roadSegmentInstantiationPosition.y,
		            			 _roadSegmentInstantiationPosition.z), Quaternion.identity);
	}

	public void Generate() {
		// Create the a new road segment at the end of the previous road segment
		_roadSegmentPrefabs = _sceneController.GetComponent<SceneController>().RoadSegmentPrefabs;

		int segmentChoice = Random.Range (1, 10);
		if (segmentChoice % 4 == 0 && _playerStartedGame) {
			var filtered = _roadSegmentPrefabs.Where(road => road.activeInHierarchy == false && road.name.Contains("Intersection"));
			Vector3 _correctedIntersectionPosition = new Vector3(_roadSegmentInstantiationPosition.x,
			                                                     _intersectionInstantiationPosition.y,
			                                                     _roadSegmentInstantiationPosition.z);
			// Instantiate (_intersectionPrefab, _correctedIntersectionPosition, Quaternion.identity);
			_roadSegmentInstantiationPosition.x += _originalInstantiationSize;
			if (filtered.Count () != 0) {
				filtered.First().transform.position = _correctedIntersectionPosition;
				filtered.First().transform.rotation = Quaternion.identity;
				filtered.First ().SetActive(true);
			} else {
				Debug.LogError("No Intersection Available");
				Generate ();
			}
		} else {
			var filtered = _roadSegmentPrefabs.Where(road => road.activeInHierarchy == false && road.name.Contains ("RoadSegment"));
			_roadSegmentInstantiationPosition.x += _originalInstantiationSize;
			// Instantiate (_roadSegmentPrefab, _roadSegmentInstantiationPosition, Quaternion.identity);
			// Debug.Log (filtered.Count ());
			if (filtered.Count () != 0) {
				filtered.First().transform.position = _roadSegmentInstantiationPosition;
				filtered.First().transform.rotation = Quaternion.identity;
				filtered.First().SetActive(true);
			} else {
				Debug.LogError("No Road Segment Available");
				Generate ();
			}
		}

	}
}
