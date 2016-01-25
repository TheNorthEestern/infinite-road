using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
	public int score;
	private GameObject _canvas;
	private GameObject _titleScreenCanvas;
	private GameObject _gameOverScreenCanvas;
	private bool _gameHasStarted = false;
	private float _distanceDrivenBeforeGameStarted;
	private float _distanceDrivenAfterGameStarted;
	private float distanceFromOrigin;
	public float totalScore = 0;
	protected static bool isPaused = false;

	void Start () {
		_canvas = GameObject.Find("HUD");
		_titleScreenCanvas = GameObject.Find ("TitleScreenCanvas");
		_gameOverScreenCanvas = GameObject.Find("GameOverScreenCanvas");
		_gameOverScreenCanvas.SetActive(false);
		_canvas.SetActive (false);
		score = 0;
		_pauseMenu.SetActive (isPaused);
	}

	void Awake() {
		_audioSource = GetComponent<AudioSource> ();
		Messenger.AddListener(GameEvent.RAN_STOP_SIGN, IncrementScore);
		Messenger.AddListener (GameEvent.APPROACHING_ONCOMING_TRAFFIC, PlayWarnSound);
		Messenger.AddListener (GameEvent.GAME_ENDED, ShowGameOverScreen);
	}

	void Update () {

		_distanceDrivenBeforeGameStarted =_player.transform.position.x - _player.GetComponent<PlayerController>().startPosition.x;

		if ( _canvas.activeSelf == true ) {
			distanceFromOrigin = ((_distanceDrivenBeforeGameStarted - _distanceDrivenAfterGameStarted)/1000);
			if ( score > 1 ) {
				distanceFromOrigin *= score;
			}
			totalScore = score;
			// _distanceText.text = _distanceTextBacking.text = distanceFromOrigin.ToString("N");
			_distanceTextBacking.text = "High Score: $" + PlayerPrefs.GetFloat("hiscore");
			_distanceText.text = "High Score: $" + PlayerPrefs.GetFloat("hiscore");
		}

		if ( (Input.GetKeyDown(KeyCode.Space) || 
		      Input.touchCount == 1 || 
		      Input.GetAxis ("Vertical") > 0 || 
		      Input.GetAxis ("Pivotal") > 0) && 
		   	  !_gameHasStarted ) {
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
		Messenger.RemoveListener (GameEvent.GAME_ENDED, ShowGameOverScreen);
	}

	public void RestartGame() {
		SceneManager.LoadScene("hillside_scene");
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

	private void ShowGameOverScreen() {
		_gameOverScreenCanvas.SetActive(true);
		// Time.timeScale = 0;
		StartCoroutine(StartNewGame ());
	}

	public IEnumerator StartNewGame() {
		float counter = 2;
		Time.timeScale = 1;
		while (counter > 0) {
			_gameOverScreenCanvas.transform.Find ("HighScoreText").GetComponent<Text>().text = counter.ToString();
			counter--;
			yield return new WaitForSeconds(1.0f);
		}
		SceneManager.LoadScene("hillside_scene");
	}

	public void OnSpeedValue(float newSpeed) {
		Debug.Log (newSpeed);
		Messenger<float>.Broadcast(GameEvent.SPEED_SLIDER_CHANGED, newSpeed);
	}

	private void IncrementScore() {
		score += 1;
		distanceFromOrigin *= score;
		_audioSource.PlayOneShot (_audioSource.clip);
		_scoreLabel.text = _scoreLabelBacking.text = score.ToString ("C0");
	}
}
