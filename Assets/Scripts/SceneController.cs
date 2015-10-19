﻿ using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
	[SerializeField] private AudioSource _audioSource;
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