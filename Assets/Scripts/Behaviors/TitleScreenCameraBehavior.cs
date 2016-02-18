using UnityEngine;
using System.Collections;

public class TitleScreenCameraBehavior : MonoBehaviour {

	public GameObject sdLogo;
	void Start () {
		iTween.ScaleTo(sdLogo, iTween.Hash("scale", new Vector3(0.3f,0.3f,0.3f), "delay", 0.2f, "easeType", "easeInSine"));
	}
}
