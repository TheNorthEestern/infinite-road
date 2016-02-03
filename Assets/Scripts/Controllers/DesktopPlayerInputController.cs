#if UNITY_STANDALONE || UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class DesktopPlayerInputController : PlayerController {

	private bool _playerDrivenInput = false;

	private void Awake() {
		Messenger.AddListener (GameEvent.PLAYER_INITIATED_GAME, SwitchToPlayerDrivenInput);
	}

	private void OnDestroy() {
		Messenger.RemoveListener (GameEvent.PLAYER_INITIATED_GAME, SwitchToPlayerDrivenInput);
	}

	public override void FixedUpdate() 
	{
		if ( Application.platform != RuntimePlatform.IPhonePlayer ||
		     Application.platform != RuntimePlatform.Android ) {
			base.FixedUpdate();
			moveVertical = (_playerDrivenInput) ? Input.GetAxis("Vertical") : 1.0f;

			if ( Input.GetAxis("Horizontal") > 0 ) {
				moveHorizontal = 1.0f;	
			} else if ( Input.GetAxis("Horizontal") < 0 ) {
				moveHorizontal = -1.0f;
			} else {
				moveHorizontal = 0;
			}

			movement = new Vector3 (moveVertical, 0.0f, 0.0f);
		}
	}

	private void SwitchToPlayerDrivenInput() {
		_playerDrivenInput = !_playerDrivenInput;
	}

}
#endif