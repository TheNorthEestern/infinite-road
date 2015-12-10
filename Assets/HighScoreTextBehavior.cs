using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreTextBehavior : MonoBehaviour {

	void Start () {
		GetComponent<Text>().text = "High Score: " + PlayerPrefs.GetFloat ("highscore").ToString ("F");
	}

}
