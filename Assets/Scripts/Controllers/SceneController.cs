using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SceneController : MonoBehaviour {
	[SerializeField] private AudioSource _audioSource;
	private float prefabAmount = 4;
	public List<GameObject> SemiPrefabs { get; private set; }
	public GameObject npc;

	public SceneController() {
		gameHasStarted = false;
	}

	public static bool gameHasStarted {get; private set;} 

	void Start() 
	{
		SemiPrefabs = new List<GameObject>();
		for (int i = 0; i < prefabAmount; i++ ) { 
			GameObject obj = (GameObject)Instantiate (npc);
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