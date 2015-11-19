#if UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class DesktopPlayerInputController : PlayerController {

	private bool _playerDrivenInput = false;

	private void Awake() {
		Messenger.AddListener (GameEvent.PLAYER_INITIATED_GAME, SwitchToPlayerDrivenInput);
	}

	private void Destroy() {
		Messenger.RemoveListener (GameEvent.PLAYER_INITIATED_GAME, SwitchToPlayerDrivenInput);
	}

	public override void FixedUpdate() 
	{
		if ( Application.platform != RuntimePlatform.IPhonePlayer ||
		     Application.platform != RuntimePlatform.Android ) {
			base.FixedUpdate();
			moveVertical = (_playerDrivenInput) ? Input.GetAxis("Vertical") : 1.0f;
			moveHorizontal = Input.GetAxis ("Horizontal");
			movement = new Vector3 (moveVertical, 0.0f, 0.0f);
		}
	}

	private void SwitchToPlayerDrivenInput() {
		_playerDrivenInput = !_playerDrivenInput;
	}

}
#endif