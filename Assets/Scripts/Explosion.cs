using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class Explosion : MonoBehaviour {
	public AudioClip SoundFile;
	public float physPushRadius = 50.0F;
	public float physPushPower = 100.0F;
	public float damageRadius = 3F;
	public GameObject particlesExplosion;

	// Use this for initialization
	void Start () {
		// Initial light range is 10
		light.range = 10f;

		//Sound
		AudioSource.PlayClipAtPoint(SoundFile, transform.position);

		// Explosion force (push other rigidbodies away)
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explosionPos, physPushRadius);
		foreach (Collider hit in colliders) {
			if (hit && hit.rigidbody && !hit.tag.Contains("Missile"))
				hit.rigidbody.AddExplosionForce(physPushPower, explosionPos, physPushRadius);
		}

		// Splashdamage
		colliders = Physics.OverlapSphere(explosionPos, damageRadius);
		foreach (Collider hit in colliders) {
			if (hit && (hit.tag == "Enemy" || hit.tag == "Player" || hit.tag == "LootCrate")){
//				Debug.Log ("FOUND " + hit.tag + " WITHIN DAMAGE RADIUS!");
				hit.SendMessage("Damage",25);
			}
		}

		//TODO Particles
		if (particlesExplosion != null) {
			Instantiate (particlesExplosion, transform.position, transform.rotation);
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Reduce light range every frame. 
		light.range = light.range - 15f * Time.deltaTime; // This value was decided by trial and error.

		// When the light range is low enough remove it.
		// TODO: Is there a better way to spawn objects than to Instantiate() an existed GameObject?
		if (light.range < 0.1 && gameObject.name.EndsWith ("(Clone)")) {
			Destroy (gameObject);
		}

	}
}