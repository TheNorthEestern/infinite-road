using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	[SerializeField] private Text _scoreLabel;
	[SerializeField] private Text _rngIndicator;
	[SerializeField] private AudioClip _scoreSound;
	private AudioSource _audioSource;
	private int _score;
	
	void Awake() {
		_audioSource = GetComponent<AudioSource> ();
		Messenger.AddListener(GameEvent.RAN_STOP_SIGN, IncrementScore);
		Messenger<int>.AddListener (GameEvent.RNG, UpdateRngIndicator);
	}

	void Start() {
		_score = 0;
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.RAN_STOP_SIGN, IncrementScore);
		Messenger<int>.RemoveListener (GameEvent.RNG, UpdateRngIndicator);
	}

	private void UpdateRngIndicator(int value) {
		string truth = (value % 4 == 0) ? "YES" : "NO";
		_rngIndicator.text = value.ToString () + "(" + truth + ")";
	}

	private void IncrementScore() {
		_score += 1;
		_audioSource.PlayOneShot (_audioSource.clip);
		_scoreLabel.text = _score.ToString ();
	}
}
