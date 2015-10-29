using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RoadSegmentBehavior : RoadCleanupBehavior {

	private Color32 winter;
	private Color32 spring;
	private Color32 summer;
	private Color32 autumn;
	private Renderer[] childRenderers;

	void Start() {

		winter = new Color32(246, 243, 237, 255);
		spring = new Color32(133, 66, 0, 255);
		summer = new Color32(137, 182, 65, 255);
		autumn = new Color32(246, 107, 0, 255);
		int currentSecond = DateTime.Now.Second;

		GameObject.Find ("Main Camera").GetComponent<Camera>().backgroundColor = Color.gray;
		childRenderers = this.transform.FindChild ("RoadsideGrass").GetComponentsInChildren<Renderer>();

		foreach (Renderer childRenderer in childRenderers) {

			if (currentSecond < 15) {
				childRenderer.material.color = winter;
			} 
			else if (currentSecond < 30) {
				childRenderer.material.color = spring;
			}
			else if (currentSecond < 45) {
				childRenderer.material.color = summer;
			} 
			else {
				childRenderer.material.color = autumn;
			}
			// go.GetComponent<Renderer>().material.color = summer;
		}
	}



}