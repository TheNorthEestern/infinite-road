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
	private bool _gameHasStarted = false;

	void OnEnable () {
		if (_gameHasStarted)
			this.Start ();
	}

	void Start () {
		Debug.Log ("HAS Started");
		GetComponent<Renderer> ().enabled = false;
		_sceneController = GameObject.Find ("SceneController");
		List<GameObject> npcPrefabs = _sceneController.GetComponent<SceneController>().NpcPrefabs;
		_gameHasStarted = true;

		var filtered = npcPrefabs.Where(npc => npc.activeInHierarchy == false);

		if (!GameObject.Find("Main Camera").GetComponent<Grayscale>().isActiveAndEnabled) {
			if (Convert.ToBoolean(filtered.Count())) {
				// _npcSpawnerSeed = UnityEngine.Random.Range (1,7);
				_npcSpawnerSeed = 2;
				Debug.Log ("Time " + Time.realtimeSinceStartup);
				if ( _npcSpawnerSeed % _npcSpawnerDeterminant == 0 ) {
					filtered.First().transform.position = transform.position;
					filtered.First().transform.rotation = transform.rotation;
					filtered.First().SetActive(true);
				}
			}
		}
	}
}
