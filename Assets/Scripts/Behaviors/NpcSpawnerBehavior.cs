using UnityEngine;
using UnityStandardAssets.ImageEffects;
using System;
using System.Linq;
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
		List<GameObject> npcPrefabs = _sceneController.GetComponent<SceneController>().NpcPrefabs;

		var filtered = npcPrefabs.Where(npc => npc.activeInHierarchy == false);

		if (Convert.ToBoolean(filtered.Count())) {
			if (!GameObject.Find("Main Camera").GetComponent<Grayscale>().isActiveAndEnabled) {
				_npcSpawnerSeed = UnityEngine.Random.Range (1,7);
				if ( _npcSpawnerSeed % _npcSpawnerDeterminant == 0 ) {
					if (transform.gameObject.name.Contains("SB")) {
						transform.parent.FindChild("Road").FindChild("RoadText").gameObject.SetActive(true);
					}
					filtered.First().transform.position = transform.position;
					filtered.First().transform.rotation = transform.rotation;
					filtered.First().SetActive(true);
				}
			}
		}
	}
}
