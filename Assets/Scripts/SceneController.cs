using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour {
	private int _score = 0;

	void Awake () {
		Application.targetFrameRate = 60;
	}

}