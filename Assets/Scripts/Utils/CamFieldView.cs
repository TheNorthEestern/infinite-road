using UnityEngine;
using System.Collections;

public class CamFieldView : MonoBehaviour {
	public float AngleView = 30.0f;
	public Transform Target = null;

	private Transform ThisTransform = null;

	void Awake() {
		ThisTransform = transform;
	}

	void Update() {
		Vector3 Forward = ThisTransform.forward.normalized;
		Vector3 ToObject = (Target.position - ThisTransform.position).normalized;

		float DotProduct = Vector3.Dot(Forward, ToObject);
		float Angle = DotProduct * 180f;

		if (Angle >= 180f - AngleView) {
			Debug.Log("Object can be seen");
		}
	}
}
