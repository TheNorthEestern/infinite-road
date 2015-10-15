using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
	[SerializeField] private Text _scoreLabel;
	private int _score;
	
	void Awake() {
		Messenger.AddListener(GameEvent.RAN_STOP_SIGN, IncrementScore);
	}

	void Start() {
		_score = 0;
	}

	private void IncrementScore() {
		_score += 1;
		_scoreLabel.text = _score.ToString () + " stops signs";
	}
}
