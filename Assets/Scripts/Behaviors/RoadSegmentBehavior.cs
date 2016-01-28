using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class RoadSegmentBehavior : MonoBehaviour {

	private Color32 winter;
	private Color32 spring;
	private Color32 summer;
	private Color32 autumn;
	private Renderer[] childRenderers;

	void OnEnable() {
		this.Start ();
	}
	 
	void Start() {

		winter = new Color32(246, 243, 237, 255);
		spring = new Color32(170, 155, 42, 255);
		summer = new Color32(137, 182, 65, 255);
		autumn = new Color32(234, 157, 46, 255);

		int currentSecond = DateTime.Now.Second;

		Camera _camera = GameObject.Find ("Main Camera").GetComponent<Camera>();
		childRenderers = this.transform.FindChild ("RoadsideGrass").GetComponentsInChildren<Renderer>();

		foreach (Renderer childRenderer in childRenderers) {
			if (currentSecond < 15) {
				// _camera.backgroundColor = winter;
				childRenderer.material.color = winter;
			} 
			else if (currentSecond < 30) {
				// _camera.backgroundColor = spring;
				childRenderer.material.color = spring;
			}
			else if (currentSecond < 45) {
				// _camera.backgroundColor = summer;
				childRenderer.material.color = summer;
			} 
			else {
				// _camera.backgroundColor = autumn ;
				childRenderer.material.color = autumn;
			}
		}
	}



}