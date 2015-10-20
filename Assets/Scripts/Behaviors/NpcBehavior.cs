using UnityEngine;
using System.Collections;

public class NpcBehavior : MonoBehaviour {
	private CharacterController _cc;
	private float speed = 5.0f;
	// Use this for initialization
	void Start () {
		_cc = GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {
		int localSpeed = Random.Range (1, 5);
		Vector3 movement = new Vector3 (0, localSpeed, 0);
		movement = Vector3.ClampMagnitude (movement, localSpeed);

		movement.x = -9.8f;
		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement);
		_cc.Move (movement);
	}

	void OnBecameInvisible() {
		Destroy (this.gameObject);
	}
}
