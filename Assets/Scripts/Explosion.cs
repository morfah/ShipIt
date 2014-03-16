using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Explosion : MonoBehaviour {
	public AudioClip SoundFile;
	public float radius = 50.0F;
	public float power = 100.0F;

	// Use this for initialization
	void Start () {
		// Initial light range is 10
		light.range = 10f;

		//Sound
		AudioSource.PlayClipAtPoint(SoundFile, transform.position);

		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
		foreach (Collider hit in colliders) {
			if (hit && hit.rigidbody && !hit.tag.Contains("Missile"))
				hit.rigidbody.AddExplosionForce(power, explosionPos, radius);
		}

		//TODO Particles
	}
	
	// Update is called once per frame
	void Update () {
		// Reduce light range every frame. 
		light.range = light.range - 15f * Time.deltaTime; // This value was decided by trial and error.

		// When the light range is low enough remove it.
		// TODO: Is there a better way to spawn objects than to Instantiate() an existed GameObject?
		if (light.range < 0.1 && gameObject.name.EndsWith("(Clone)"))
			Destroy(gameObject);
	}
}
