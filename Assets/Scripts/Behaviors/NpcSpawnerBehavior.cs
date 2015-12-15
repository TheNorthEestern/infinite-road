using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System.Collections;
using System.Collections.Generic;

public class NpcSpawnerBehavior : MonoBehaviour {
	// Together with _npcSpawnerSeed, this variable determines
	// how often an NPC is spawned
	private GameObject _sceneController;
	private int _npcSpawnerDeterminant = 2;
	private int _npcSpawnerSeed;
	private string[] npcModelNames = new string[] {"NPCSemi", "NPC"};

	void Start () {
		GetComponent<Renderer> ().enabled = false;
		_sceneController = GameObject.Find ("SceneController");
		List<GameObject> npcPrefabs = _sceneController.GetComponent<SceneController>().SemiPrefabs;

		for ( int i = 0; i < npcPrefabs.Count; i++ ) {
			if (!npcPrefabs[i].activeInHierarchy) {
				if (!GameObject.Find("Main Camera").GetComponent<Grayscale>().isActiveAndEnabled) {
					_npcSpawnerSeed = Random.Range (1,7);
					int randomModelName = Random.Range (0,2);
					if ( _npcSpawnerSeed % _npcSpawnerDeterminant == 0 ) {

						if (transform.gameObject.name.Contains("SB")) {
							transform.parent.FindChild("Road").FindChild("RoadText").gameObject.SetActive(true);
						}

						npcPrefabs[i].transform.position = transform.position;
						npcPrefabs[i].transform.rotation = transform.rotation;
						npcPrefabs[i].SetActive(true);
						break;
					}
				}
			}
		}

		/* if (!GameObject.Find("Main Camera").GetComponent<Grayscale>().isActiveAndEnabled) {
			_npcSpawnerSeed = Random.Range (1,7);
			int randomModelName = Random.Range (0,2);
			if ( _npcSpawnerSeed % _npcSpawnerDeterminant == 0 ) {

				if (transform.gameObject.name.Contains("SB")) {
					transform.parent.FindChild("Road").FindChild("RoadText").gameObject.SetActive(true);
				}

				GameObject npc = Resources.Load ("Prefabs/" + npcModelNames[randomModelName]) as GameObject;
				Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				Instantiate (npc, newTransform, transform.localRotation);
			}
		} */
	}
}
