using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreTextBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = PlayerPrefs.GetFloat ("highscore").ToString ("F");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
