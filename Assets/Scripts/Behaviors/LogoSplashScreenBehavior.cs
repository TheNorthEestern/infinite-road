using UnityEngine;
using UnityEngine.SceneManagement;
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
		yield return new WaitForSeconds(5);
		SceneManager.LoadScene("hillside_scene");
	}
}
