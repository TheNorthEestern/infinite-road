using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	protected static AudioSource _audioSource;
	protected static Rigidbody _rb;
	protected static bool _sonicBoom = false;
	protected static float speed = 10.0f;
	protected static float moveVertical;
	protected static float moveHorizontal;
	protected const float GRAVITY = -9.8f;
	protected static Vector3 movement;
	protected static Vector3 restrictor;

	void Start () 
	{
		Debug.Log ("Start!");
		_audioSource = GetComponent<AudioSource> ();
		_rb = GetComponent<Rigidbody> ();
		_rb.freezeRotation = true;
	}

	public virtual void FixedUpdate() 
	{
		restrictor = transform.position;
		restrictor.z = Mathf.Clamp (restrictor.z, -5.5f, -3.3f);
		_rb.transform.position = restrictor;

		Debug.Log (moveVertical);
		if (moveVertical < 0) {
			_rb.drag += .5f;
		} else if (moveVertical == 0) {
			_rb.drag += .002f;
		} else {
			_rb.drag = 0;
		}
		
		if (_rb.velocity.x >= 70.0f && _sonicBoom == false) {
			_audioSource.PlayOneShot (_audioSource.clip);
			_sonicBoom = true;
		} else if (_rb.velocity.x <= 70.0f) {
			_sonicBoom = false;
		}
		
		_rb.velocity = Vector3.ClampMagnitude (_rb.velocity, 100.0f);
		_rb.AddForce (movement * speed);
	}

	void OnCollisionEnter(Collision other) {
		Debug.Log (other.gameObject.name);
		if (other.gameObject.tag == "NPC") {
			Application.LoadLevel ("hillside_scene");
		}
	}

}
