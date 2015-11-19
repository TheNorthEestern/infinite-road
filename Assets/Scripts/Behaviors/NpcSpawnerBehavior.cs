using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;

public class NpcSpawnerBehavior : MonoBehaviour {
	// Together with _npcSpawnerSeed, this variable determines
	// how often an NPC is spawned
	private GameObject _sceneController;
	private int _npcSpawnerDeterminant = 2;
	private int _npcSpawnerSeed;
	private bool _isActive = false;
	private string[] npcModelNames = new string[] {"NPCSemi", "NPC"};

	void Start () {
		GetComponent<Renderer> ().enabled = false;
		if (!GameObject.Find("Main Camera").GetComponent<Grayscale>().isActiveAndEnabled) {
			_npcSpawnerSeed = Random.Range (1,7);
			int randomModelName = Random.Range (0,2);
			if ( _npcSpawnerSeed % _npcSpawnerDeterminant == 0 ) {
				GameObject npc = Resources.Load ("Prefabs/" + npcModelNames[randomModelName]) as GameObject;
				Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				Instantiate (npc, newTransform, transform.localRotation);
			}
		}
	}
}
