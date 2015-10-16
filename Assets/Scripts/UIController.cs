using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	[SerializeField] private Text _scoreLabel;
	[SerializeField] private AudioClip _scoreSound;
	private AudioSource _audioSource;
	private int _score;
	
	void Awake() {
		_audioSource = GetComponent<AudioSource> ();
		Messenger.AddListener(GameEvent.RAN_STOP_SIGN, IncrementScore);
	}

	void Start() {
		_score = 0;
	}

	private void IncrementScore() {
		_score += 1;
		_audioSource.PlayOneShot (_audioSource.clip);
		_scoreLabel.text = _score.ToString ();
	}
}
