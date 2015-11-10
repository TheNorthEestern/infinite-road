using UnityEngine;
using System.Collections;

public class ParentedObjectBehavior : MonoBehaviour {
	protected void CheckIfOnRoad() {

		if (transform.position.y < 0) {
			Destroy (this.gameObject);
		}
	}
}
