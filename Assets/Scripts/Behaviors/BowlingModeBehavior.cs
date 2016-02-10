using UnityEngine;
using System.Collections;

public class BowlingModeBehavior : PlayerController {
	public void ResetGravity() {
		gravity = 0;
		Debug.Log("BOWLING!");
	}
}
