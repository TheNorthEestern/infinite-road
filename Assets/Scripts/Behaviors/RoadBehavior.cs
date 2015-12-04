using UnityEngine;
using System.Collections;

public class RoadBehavior : MonoBehaviour {
	private MeshRenderer _meshRenderer;
	public Material[] materialList;
	private string[] quipList = { " DOWN\nSLOW", " UP\nSPEED" };
	private int _textureChoice;

	void Start () {		                          
		TextMesh t = transform.FindChild("RoadText").GetComponent<TextMesh>();
		_textureChoice = Random.Range (0, 2);
		t.text = Time.realtimeSinceStartup.ToString ("F");
		_meshRenderer = GetComponent<MeshRenderer> ();
		_meshRenderer.material = materialList [_textureChoice];
	}
}
