using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	[SerializeField] private Text _scoreLabel;
	[SerializeField] private Text _scoreLabelBacking;
	[SerializeField] private Text _distanceText;
	[SerializeField] private Text _distanceTextBacking;
	[SerializeField] private Text _rngIndicator;
	[SerializeField] private AudioClip _warnSound;
	[SerializeField] private GameObject _pauseMenu;
	[SerializeField] private GameObject _player;
	private AudioSource _audioSource;
	private int _score;
	private GameObject _canvas;
	private GameObject _titleScreenCanvas;
	private bool _gameHasStarted = false;
	private float _distanceDrivenBeforeGameStarted;
	private float _distanceDrivenAfterGameStarted;
	private float distanceFromOrigin;
	public float totalScore = 0;
	protected static bool isPaused = false;

	void Start () {
		_canvas = GameObject.Find("HUD");
		_titleScreenCanvas = GameObject.Find ("TitleScreenCanvas");
		_canvas.SetActive (false);
		_score = 0;
		_pauseMenu.SetActive (isPaused);
	}

	void Awake() {
		_audioSource = GetComponent<AudioSource> ();
		Messenger.AddListener(GameEvent.RAN_STOP_SIGN, IncrementScore);
		Messenger.AddListener (GameEvent.APPROACHING_ONCOMING_TRAFFIC, PlayWarnSound);
	}

	void Update () {

		_distanceDrivenBeforeGameStarted =_player.transform.position.x - _player.GetComponent<PlayerController>().startPosition.x;

		if ( _canvas.activeSelf == true ) {
			distanceFromOrigin = ((_distanceDrivenBeforeGameStarted - _distanceDrivenAfterGameStarted)/1000);
			if ( _score > 1 ) {
				distanceFromOrigin *= _score;
			}
			totalScore = distanceFromOrigin;
			_distanceText.text = _distanceTextBacking.text = distanceFromOrigin.ToString("N");
		}

		if ( (Input.GetKeyDown(KeyCode.Space) || Input.touchCount == 1) && !_gameHasStarted ) {
			_gameHasStarted = true;
			_distanceDrivenAfterGameStarted	= _player.transform.position.x - _player.GetComponent<PlayerController>().startPosition.x;
			Messenger.Broadcast (GameEvent.PLAYER_INITIATED_GAME);
			_canvas.SetActive (true);
			_titleScreenCanvas.SetActive(false);
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

	public void OnSpeedValue(float newSpeed) {
		Debug.Log (newSpeed);
		Messenger<float>.Broadcast(GameEvent.SPEED_SLIDER_CHANGED, newSpeed);
	}

	private void IncrementScore() {
		_score += 1;
		distanceFromOrigin *= _score;
		_audioSource.PlayOneShot (_audioSource.clip);
		_scoreLabel.text = _scoreLabelBacking.text = '$' + _score.ToString ();
	}
}
