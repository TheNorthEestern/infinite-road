﻿using UnityEngine;
using System.Collections;
using System;

public class ColorRandomizerBehavior : MonoBehaviour {

	private Renderer objectRenderer;

	private Color32 generateRandomColor(Color32 mix) {
		System.Random random = new System.Random();
		byte red = (byte) random.Next(256);
		byte green = (byte) random.Next(256);
		byte blue = (byte) random.Next(256);

		if (!mix.Equals(null)) {
			red = (byte) ((red + mix.r) / 2);
			green = (byte) ((green + mix.g) / 2);
			blue = (byte) ((blue + mix.b) / 2);
		}

		Color32 color = new Color32(red, green, blue, 255);
		return color;
	}

	void Start () {
		objectRenderer = GetComponent<Renderer>();	
		objectRenderer.material.color = generateRandomColor(new Color32(0, 0, 0, 255));
	}

	void OnEnable() {
		this.Start();
		objectRenderer.material.color = generateRandomColor(new Color32(0, 0, 0, 255));
	}

}