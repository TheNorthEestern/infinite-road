using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadBehavior : MonoBehaviour {
	private MeshRenderer _meshRenderer;
	private GameObject _coin;
	private GameObject _sceneController;
	private List<GameObject> _coinPrefabs;
	public Material[] materialList;
	private	float[] coinPositions = { -5.35f, -3.0f };
	private int _textureChoice;
	private bool _gameHasStarted = false;

	void OnEnable() {
		if (_gameHasStarted) 
			this.Start ();
	}

	void Start () {		                          
		_sceneController = GameObject.Find("SceneController");
		_coinPrefabs = _sceneController.GetComponent<SceneController>().CoinPrefabs;
		_gameHasStarted = true;

		if ( _sceneController.GetComponent<RoadSegmentGenerator>()._playerStartedGame ) {
			PlaceCoin();
		}

		_textureChoice = Random.Range (0, 2);
		_meshRenderer = GetComponent<MeshRenderer> ();
		_meshRenderer.material = materialList [_textureChoice];
	}

	private void PlaceCoin() {
		/* foreach (GameObject coin in _coinPrefabs) {
			if (!coin.activeInHierarchy) {
				Vector3 newPosition = new Vector3(transform.position.x + 3.0f, transform.position.y, coinPositions[Random.Range (0,2)]);
				coin.transform.position = newPosition;
				coin.transform.rotation = Quaternion.Inverse(transform.localRotation);
				coin.SetActive(true);
				break;
			}
		}*/
		_coin = Resources.Load ("Prefabs/CoinPickup", typeof(GameObject)) as GameObject;
		Vector3 newPosition = new Vector3(transform.position.x + 3.0f, transform.position.y, coinPositions[Random.Range (0,2)]);
		_coin = Instantiate(_coin, newPosition, Quaternion.Inverse (transform.localRotation)) as GameObject;
	}
}
