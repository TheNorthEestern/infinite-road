using UnityEngine;
using System;
using System.Collections;

public class SceneController : MonoBehaviour {
	[SerializeField] private AudioSource _audioSource;

	public SceneController() {
		gameHasStarted = false;
	}

	public static bool gameHasStarted {get; private set;} 

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