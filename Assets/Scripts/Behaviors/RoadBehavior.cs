using UnityEngine;
using System.Collections;

public class RoadBehavior : MonoBehaviour {
	private MeshRenderer _meshRenderer;
	private GameObject _coin;
	public Material[] materialList;
	private	float[] coinPositions = { -5.35f, -3.0f };
	private int _textureChoice;

	void Start () {		                          
		if ( GameObject.Find ("SceneController").GetComponent<RoadSegmentGenerator>()._playerStartedGame ) {
			PlaceCoin();
		}
		TextMesh t = transform.FindChild("RoadText").GetComponent<TextMesh>();
		_textureChoice = Random.Range (0, 2);
		t.text = Time.realtimeSinceStartup.ToString ("F");
		_meshRenderer = GetComponent<MeshRenderer> ();
		_meshRenderer.material = materialList [_textureChoice];
	}

	private void PlaceCoin() {
		_coin = Resources.Load ("Prefabs/CoinPickup", typeof(GameObject)) as GameObject;
		Vector3 newPosition = new Vector3(transform.position.x + 3.0f, transform.position.y, coinPositions[Random.Range (0,2)]);
		_coin = Instantiate(_coin, newPosition, Quaternion.Inverse (transform.localRotation)) as GameObject;
	}
}
