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
	private GameObject _canvas;
	private GameObject _titleScreenCanvas;
	protected static bool isPaused = false;

	void Start () {
		_canvas = GameObject.Find("Canvas");
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
		/* if ( _score != 0 && _score % 10 == 0 ) {
			Messenger.Broadcast(GameEvent.PLAYER_GOT_TEN_PASSES);
		} */

		if ( Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0 ) {
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
//		int randX = Random.Range(0, Screen.width);
//		int randY = Random.Range (0, Screen.height);
//
//		GameObject plusOne = Resources.Load ("Prefabs/PlusOne", typeof(GameObject)) as GameObject;
//		Vector3 textPos = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), plusOne.transform.position.z);
//		plusOne.transform.position = textPos;
//		Instantiate (plusOne);
		_score += 1;
		_audioSource.PlayOneShot (_audioSource.clip);
		_scoreLabel.text = _scoreLabelBacking.text = _score.ToString ();
	}
}
