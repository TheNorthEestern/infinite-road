using UnityEngine;
using System.Collections;

public class NpcSpawnerBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().enabled = false;
		Debug.Log ("NPCSpawner transform: " + transform.position);
		GameObject npc = Resources.Load ("Prefabs/npc") as GameObject;
		Vector3 newTransform = new Vector3(transform.position.x, npc.transform.position.y, transform.position.z);
		Instantiate (npc, newTransform, npc.transform.localRotation);
	}
	
	// Update is called once per frame
	void Update () {
		// StartCoroutine ("GenNpc");
	}

	IEnumerator GenNpc() 
	{
		GameObject npc = Resources.Load ("Prefabs/npc") as GameObject;
		Instantiate (npc, transform.position, npc.transform.localRotation);
		yield return new WaitForSeconds(3);
	}
}
