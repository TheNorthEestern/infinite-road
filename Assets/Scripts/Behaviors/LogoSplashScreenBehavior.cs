using UnityEngine;
using System.Collections;

public class LogoSplashScreenBehavior : MonoBehaviour {

	void Start() {
		// StartCoroutine (TitleScreenDuration ());
	}

	void Update() {
		if (!Application.isShowingSplashScreen) {
			StartCoroutine (TitleScreenDuration ());
		}
	}

	private IEnumerator TitleScreenDuration() {
		yield return new WaitForSeconds(3);
		Application.LoadLevel("hillside_scene");
	}
}
