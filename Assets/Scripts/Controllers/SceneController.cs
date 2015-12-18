using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SceneController : MonoBehaviour {
	[SerializeField] private AudioSource _audioSource;
	private float prefabAmount = 8;
	public List<GameObject> SemiPrefabs { get; private set; }
	public GameObject smallNpc;
	public GameObject largeNpc;
	private GameObject[] npcChoices;

	public SceneController() {
		gameHasStarted = false;
	}

	public static bool gameHasStarted {get; private set;} 

	void Start() 
	{
		npcChoices = new GameObject[2] {smallNpc, largeNpc};
		SemiPrefabs = new List<GameObject>();
		for (int i = 0; i < prefabAmount; i++ ) { 
			GameObject obj = (GameObject)Instantiate (npcChoices[UnityEngine.Random.Range(0,2)]);
			obj.SetActive(false);
		 	SemiPrefabs.Add (obj);
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
		_audioSource.PlayOneShot (_audioSource.clip);
	}

}