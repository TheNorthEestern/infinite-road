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
	public AudioSource pickupSound;
	public AudioSource tireScreech;
	private GameObject _canvas;
	private GameObject _titleScreenCanvas;
	private GameObject _gameOverScreenCanvas;
	private bool _gameHasStarted = false;
	private bool _comboChainStarted = false;
	private float _distanceDrivenBeforeGameStarted;
	private float _distanceDrivenAfterGameStarted;
	private float distanceFromOrigin;
	public GameObject messageText;
	public GameObject messageTextBacking;
	public GameObject comboMessageText;
	public GameObject comboMessageTextBacking;
	public Text multiplierText;
	public Text multiplierTextBacking;
	public float totalScore = 0;
	public int comboCounter = 0;
	public int score;
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
		Messenger.AddListener (GameEvent.APPROACHING_ONCOMING_TRAFFIC, PlayWarnSound);
		Messenger.AddListener (GameEvent.GAME_ENDED, ShowGameOverScreen);
		Messenger.AddListener (GameEvent.PLAYER_STOPPED, DecrementScore);
		Messenger<float>.AddListener(GameEvent.RAN_STOP_SIGN, IncrementScore);
		Messenger<bool>.AddListener (GameEvent.COIN_EVENT, UpdateComboStatus);
	}

	void OnDestroy() {
		Messenger.RemoveListener(GameEvent.APPROACHING_ONCOMING_TRAFFIC, PlayWarnSound);
		Messenger.RemoveListener (GameEvent.GAME_ENDED, ShowGameOverScreen);
		Messenger.RemoveListener(GameEvent.PLAYER_STOPPED, DecrementScore);
		Messenger<float>.RemoveListener (GameEvent.RAN_STOP_SIGN, IncrementScore);
		Messenger<bool>.RemoveListener (GameEvent.COIN_EVENT, UpdateComboStatus);
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
			_distanceTextBacking.text = _distanceText.text = "High Score: " + PlayerPrefs.GetFloat("hiscore").ToString("C0");
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
		if (!pickupSound.isPlaying) {
			pickupSound.PlayOneShot (_warnSound);
		}
	}

	public void PlayScreechSound() {
		if (!tireScreech.isPlaying) {
			// tireScreech.pitch = Random.Range(0.85f, 1.0f);
			tireScreech.Play();		
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
			counter--;
			yield return new WaitForSeconds(1.0f);
		}
		SceneManager.LoadScene("hillside_scene");
	}

	public void OnSpeedValue(float newSpeed) {
		Debug.Log (newSpeed);
		Messenger<float>.Broadcast(GameEvent.SPEED_SLIDER_CHANGED, newSpeed);
	}

	private void SetScoreLabelText(int text) {
		_scoreLabel.text = _scoreLabelBacking.text = text.ToString("C0");
	}

	private void SetMultiplierText(int text) {
		if (text == 0) {
			multiplierText.text = multiplierTextBacking.text = "";
		} else {
			multiplierText.text = multiplierTextBacking.text = text.ToString() + "x";
		}
	}

	private void IncrementScore(float playerPosition) {
		int chainAmount = comboCounter * (comboCounter + 1);
		score += (comboCounter > 1 && score > 1) ? chainAmount : 1;
		if ( comboCounter >= 1 ) {
			GameObject scoreText = Instantiate(Resources.Load("Prefabs/MessageText", typeof(GameObject))) as GameObject;
			scoreText.GetComponent<MessageTextBehavior>().MessageText = "+" + chainAmount;
		}
		distanceFromOrigin *= score;
		pickupSound.PlayOneShot (pickupSound.clip);
		SetScoreLabelText(score);
	}

	private void DecrementScore() {
		score -= 1;	
		SetScoreLabelText(score);
	}

	private void UpdateComboStatus(bool playerDidMissCoin) {
		if (!playerDidMissCoin) {
			_comboChainStarted = true;
			comboCounter += 1;
			SetMultiplierText(comboCounter);
		} else if (playerDidMissCoin && _comboChainStarted) {
			StartCoroutine(ShowBreakerMessage());
			SetMultiplierText(0);
			_comboChainStarted = false;
		}
	}

	private IEnumerator ShowBreakerMessage() {
		// StartCoroutine(MoveMessageOnScreen(comboCounter));
		if (comboCounter >= 5) {
			comboMessageTextBacking.GetComponent<Text>().text = 
			comboMessageText.GetComponent<Text>().text = "Combo Breaker!\n" + comboCounter + " Coin Streak\n" + comboCounter + "X multipler = " + comboCounter + " x $25 = " + comboCounter * 25;
			int bonus = comboCounter * 25;
			score += bonus;
			SetScoreLabelText(score);
			comboCounter = 0;
			iTween.MoveTo(comboMessageText, iTween.Hash("x", 280.0f, "easeType", "easeInOutBack", "loopType", "none"));
			yield return new WaitForSeconds(3);		
			iTween.MoveTo(comboMessageText, iTween.Hash("x", -350.0f, "easeType", "easeInOutBack", "loopType", "none"));
		} 
		comboCounter = 0;
		yield return null;
	}

}
