using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SceneController : MonoBehaviour {
	[SerializeField] private AudioSource _audioSource;
	private float npcPrefabCount = 8;
	private float roadSegmentPrefabCount = 16;
	private float coinPrefabCount = 6;
	public List<GameObject> NpcPrefabs { get; private set; }
	public List<GameObject> RoadSegmentPrefabs { get; private set; }
	public List<GameObject> CoinPrefabs { get; private set; }
	public GameObject roadSegment;
	public GameObject intersectionSegment;
	public GameObject coinPrefab;
	[SerializeField] private GameObject[] npcs;
	private GameObject[] roadSegmentChoices;

	public SceneController() {
		gameHasStarted = false;
	}

	public static bool gameHasStarted {get; private set;} 

	void Start() 
	{
		roadSegmentChoices = new GameObject[2] {roadSegment, intersectionSegment};

		NpcPrefabs 		   = new List<GameObject>();
		RoadSegmentPrefabs = new List<GameObject>();
		CoinPrefabs 	   = new List<GameObject>();

		for (int i = 0; i < roadSegmentPrefabCount; i++) {
			GameObject obj;
			if ( i % 2 == 0 ) {
				obj = (GameObject)Instantiate (roadSegmentChoices[0]);
			} else {
				obj = (GameObject)Instantiate (roadSegmentChoices[1]);
			}
			obj.SetActive(false);
			RoadSegmentPrefabs.Add (obj);
		}

		for (int i = 0; i < npcPrefabCount; i++) { 
			GameObject obj = (GameObject)Instantiate (npcs[UnityEngine.Random.Range(0,5)]);
			obj.SetActive(false);
		 	NpcPrefabs.Add (obj);
		}

		for (int i = 0; i < coinPrefabCount; i++) {
			GameObject obj = (GameObject)Instantiate (coinPrefab);
			obj.SetActive(false);
			CoinPrefabs.Add(obj);
		}
	}

	void Awake () 
	{
		_audioSource = GetComponent<AudioSource> ();
		Application.targetFrameRate = 60;
		Messenger.AddListener (GameEvent.CHRACTER_STARTED_SPEEDING, MakeNoise);
	}
		
	public void MakeNoise() 
	{
		_audioSource.Play();
	}

}