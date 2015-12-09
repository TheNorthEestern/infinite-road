using UnityEngine;
using System.Collections;

public class TitleScreenCameraBehavior : MonoBehaviour {

	void Start () {
		iTween.CameraFadeAdd();
		iTween.CameraFadeTo (iTween.Hash ("amount", 1.0f, "time", 2.0f, "delay", 3.0f));
		iTween.CameraFadeAdd();
		iTween.CameraFadeTo(iTween.Hash("amount", 1.0f, "time", 2.0f, "delay", 3.0f));
	}
}
