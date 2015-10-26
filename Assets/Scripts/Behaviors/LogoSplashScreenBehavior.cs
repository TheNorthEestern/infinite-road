using UnityEngine;
using System.Collections;

public class LogoSplashScreenBehavior : MonoBehaviour {

	void Start() {
		StartCoroutine (TitleScreenDuration ());
	}

	private IEnumerator TitleScreenDuration() {
		yield return new WaitForSeconds(5);
		Application.LoadLevel ("hillside_scene");
	}
}
