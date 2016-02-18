using UnityEngine;
using System.Collections;

public class SpecialEffects : MonoBehaviour {
	private static SpecialEffects instance;
	public ParticleSystem explosionEffect;
	public GameObject trailPrefab;

	void Awake() {
		instance = this;
	}

	public static ParticleSystem MakeExplosion(Vector3 position) {
		ParticleSystem effect = Instantiate(instance.explosionEffect) as ParticleSystem;
		effect.transform.position = position;
		Destroy(effect.gameObject, effect.duration);
		return effect;
	}

	public static GameObject MakeTrail(Vector3 position) {
		GameObject trail = Instantiate(instance.trailPrefab) as GameObject;
		trail.transform.position = position;

		return trail;
	}
}
