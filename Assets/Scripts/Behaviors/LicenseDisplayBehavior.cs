using SDGameEnums;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class LicenseDisplayBehavior : MonoBehaviour {
	private Sprite[] sprites;
	private string enumNames;
	private Dictionary<string, Sprite> licensePairs;
	public Image licenseImage;
		
	void Start () { 
		licensePairs = new Dictionary<string, Sprite>();
		sprites = Resources.LoadAll<Sprite>("Sprites/Stroke");	
		licenseImage = GetComponent<Image>();
		foreach (Sprite sprite in sprites) {
			licensePairs.Add(sprite.name, sprite);
		}
		int lastPlayerHighScore = (int)PlayerPrefs.GetFloat("hiscore");
			if ( lastPlayerHighScore >= (int)LicenseTypes.E)
				licenseImage.sprite = licensePairs[Enum.GetName(typeof(LicenseTypes), LicenseTypes.E)];

			if (lastPlayerHighScore >= (int)LicenseTypes.D)
				licenseImage.sprite = licensePairs[Enum.GetName(typeof(LicenseTypes), LicenseTypes.D)];

			if (lastPlayerHighScore >= (int)LicenseTypes.C)
				licenseImage.sprite = licensePairs[Enum.GetName(typeof(LicenseTypes), LicenseTypes.C)];

			if (lastPlayerHighScore >= (int)LicenseTypes.B)
				licenseImage.sprite = licensePairs[Enum.GetName(typeof(LicenseTypes), LicenseTypes.B)];

			if (lastPlayerHighScore >= (int)LicenseTypes.A)
				licenseImage.sprite = licensePairs[Enum.GetName(typeof(LicenseTypes), LicenseTypes.A)];

			if (lastPlayerHighScore >= (int)LicenseTypes.S)
				licenseImage.sprite = licensePairs[Enum.GetName(typeof(LicenseTypes), LicenseTypes.S)];

			if (lastPlayerHighScore >= (int)LicenseTypes.SB)
				licenseImage.sprite = licensePairs[Enum.GetName(typeof(LicenseTypes), LicenseTypes.SB)];

			if (lastPlayerHighScore >= (int)LicenseTypes.SA)
				licenseImage.sprite = licensePairs[Enum.GetName(typeof(LicenseTypes), LicenseTypes.SA)];

			if (lastPlayerHighScore >= (int)LicenseTypes.Z)
				licenseImage.sprite = licensePairs[Enum.GetName(typeof(LicenseTypes), LicenseTypes.Z)];

			if (lastPlayerHighScore >= (int)LicenseTypes.Q)
				licenseImage.sprite = licensePairs[Enum.GetName(typeof(LicenseTypes), LicenseTypes.Q)];

		// GetComponent<Image>().sprite = sprites[0];	
		// StartCoroutine(ChangeLicenseClass());
	}

	IEnumerator ChangeLicenseClass() {
		int spriteNumber = UnityEngine.Random.Range(0, sprites.Length);
		GetComponent<Image>().sprite = sprites[spriteNumber];
		yield return new WaitForSeconds(3);
		StartCoroutine(ChangeLicenseClass());
	}
}
