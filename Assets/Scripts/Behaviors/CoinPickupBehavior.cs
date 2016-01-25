using UnityEngine;
using System.Collections;

public class CoinPickupBehavior : MonoBehaviour {
	void OnBecameInvisible() {
		Discard();
	}

	void Discard() {
		gameObject.SetActive(false);
	}
}
