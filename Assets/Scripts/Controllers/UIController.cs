using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	[SerializeField] private Text _scoreLabel;
	[SerializeField] private Text _scoreLabelBacking;
	[SerializeField] private Text _rngIndicator;
	[SerializeField] private AudioClip _warnSound;
	[SerializeField] private GameObject _pauseMenu;
	private AudioSource _audioSource;
	private int _score;
	protected static bool isPaused = false;

	void Start () {
		_score = 0;
		_pauseMenu.SetActive (isPaused);
	}

	void Awake() {
		_audioSource = GetComponent<AudioSource> ();
		Messenger.AddListener(GameEvent.RAN_STOP_SIGN, IncrementScore);
		Messenger.AddListener (GameEvent.APPROACHING_ONCOMING_TRAFFIC, PlayWarnSound);
	}

	void Update () {
		if ( Input.GetKeyDown(KeyCode.Space) ) {
			Pause ();
		}
	}

	void OnDestroy() {
		Messenger.RemoveListener (GameEvent.RAN_STOP_SIGN, IncrementScore);
		Messenger.RemoveListener(GameEvent.APPROACHING_ONCOMING_TRAFFIC, PlayWarnSound);
	}

	public void Pause() {
		if(!isPaused) {
			Time.timeScale = 0;
			isPaused = true;
			_pauseMenu.SetActive (isPaused);
		} else if (isPaused) {
			Time.timeScale = 1;
			isPaused = false;
			_pauseMenu.SetActive (isPaused);
		}
	}

	private void PlayWarnSound() { 
		if (!_audioSource.isPlaying) {
			_audioSource.PlayOneShot (_warnSound);
		}
	}

	private void IncrementScore() {
		_score += 1;
		_audioSource.PlayOneShot (_audioSource.clip);
		_scoreLabel.text = _scoreLabelBacking.text = _score.ToString ();
	}
}
