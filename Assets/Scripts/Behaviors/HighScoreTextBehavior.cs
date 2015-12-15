using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreTextBehavior : MonoBehaviour {

	void Start () {
		GetComponent<Text>().text = "Your Score: " + PlayerPrefs.GetFloat ("CurrentScore").ToString ("F")
									+ "\nHigh Score: " + PlayerPrefs.GetFloat ("highscore").ToString ("F");
	}

}
