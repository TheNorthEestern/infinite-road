using UnityEngine;
using System.Collections;

public class MessageTextBehavior : MonoBehaviour {

	private GameObject _player;
	public string MessageText {
		get { return GetComponent<TextMesh>().text; }
		set {
			GetComponent<TextMesh>().text = value;	
			transform.FindChild("MessageTextBacking").GetComponent<TextMesh>().text = value;
		}
	}

	void Start() {
		_player = GameObject.Find("student_driver");
	}

	void Update() {
		Vector3 textPos = new Vector3(_player.transform.position.x + 1.5f,
									  _player.transform.position.y,
									  _player.transform.position.z);
		transform.position = textPos;
	}

	void OnEnable() {
		gameObject.SetActive(true);
		StartCoroutine(Discard());
	}

	private IEnumerator Discard() {
		// iTween.FadeTo(gameObject, iTween.Hash("alpha",0.0f, "delay", 0.5f));
		iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(0,0,0), "delay", 0.2f));
		yield return new WaitForSeconds(2);
		Destroy(this.gameObject);
	}

}
