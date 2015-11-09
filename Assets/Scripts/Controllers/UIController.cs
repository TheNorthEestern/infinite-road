using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	[SerializeField] private Text _scoreLabel;
	[SerializeField] private Text _rngIndicator;
	[SerializeField] private AudioClip _warnSound;
	private AudioSource _audioSource;
	private int _score;
	
	void Awake() {
		_audioSource = GetComponent<AudioSource> ();
		Messenger.AddListener(GameEvent.RAN_STOP_SIGN, IncrementScore);
		Messenger.AddListener (GameEvent.APPROACHING_ONCOMING_TRAFFIC, PlayWarnSound);
	}

	void Start() {
		_score = 0;
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.RAN_STOP_SIGN, IncrementScore);
		Messenger.RemoveListener(GameEvent.APPROACHING_ONCOMING_TRAFFIC, PlayWarnSound);
	}

	private void PlayWarnSound() { 
		if (!_audioSource.isPlaying) {
			_audioSource.PlayOneShot (_warnSound);
		}
	}

	private void IncrementScore() {
		_score += 1;
		_audioSource.PlayOneShot (_audioSource.clip);
		_scoreLabel.text = _score.ToString ();
	}
}
