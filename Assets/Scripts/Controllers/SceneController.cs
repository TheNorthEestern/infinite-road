using UnityEngine;
using System;
using System.Collections;

public class SceneController : MonoBehaviour {
	[SerializeField] private AudioSource _audioSource;
	private bool isPaused = false;

	void Awake () 
	{
		_audioSource = GetComponent<AudioSource> ();
		Application.targetFrameRate = 60;
		Messenger.AddListener (GameEvent.CHRACTER_STARTED_SPEEDING, MakeNoise);
	}

	void Update(){
		if ( Input.GetKeyDown(KeyCode.Space) ) {
			Debug.Log("Spacesu GA");
			if(!isPaused) {
				Time.timeScale = 0;
				isPaused = true;
			} else if (isPaused) {
				Time.timeScale = 1;
				isPaused = false;
			}
		}
	}

	public void MakeNoise() 
	{
		_audioSource.PlayOneShot (_audioSource.clip);
	}

}