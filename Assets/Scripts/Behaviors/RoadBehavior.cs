using UnityEngine;
using System.Collections;

public class RoadBehavior : MonoBehaviour {
	private MeshRenderer _meshRenderer;
	public Material[] materialList;
	private int _textureChoice;

	void Start () {		                          
		_textureChoice = Random.Range (0, 2);
		_meshRenderer = GetComponent<MeshRenderer> ();
		_meshRenderer.material = materialList [_textureChoice];
	}
}
