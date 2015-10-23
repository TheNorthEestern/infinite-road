using UnityEngine;
using System.Collections;

public class NpcSpawnerBehavior : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().enabled = false;
		GameObject npc = Resources.Load ("Prefabs/NPC") as GameObject;
		Vector3 newTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		Instantiate (npc, newTransform, transform.localRotation);
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
