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
	[HideInInspector]
	public int Level = 1;
	[HideInInspector]
	public bool Friendly = true;

	private const float BASE_EXPLOSION_DAMAGE = 25.0f;

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
			//Debug.Log (tag + " hit " + hit.tag);
			//Debug.Log ("Is explosion friendly? " + Friendly + " Level of Explosion? " + Level);
			if (hit){
				// A Explosion hit Lootcrate
				if (hit.tag == "SmallLootCrate" || hit.tag == "LargeLootCrate") {
					hit.SendMessage("Damage", BASE_EXPLOSION_DAMAGE);
				}

				// Friendly Explosion caused by a friendly missile.
				if (Friendly) {
					// Hit Enemy
					if (hit.tag == "Enemy") {
						Enemy enemy = hit.GetComponent<Enemy>();
						float Damage = BASE_EXPLOSION_DAMAGE * ((float)Level / (float)enemy.GetEnemyLevel());
						hit.SendMessage("Damage", Damage);
					}
					// Hit Player
					else if (hit.tag == "Player") {
//						hit.SendMessage("Damage", BASE_EXPLOSION_DAMAGE);
					}
				}

				// Enemy explosion caused by an enemy missile.
				else {
					// Hit Enemy
					if (hit.tag == "Enemy") {
//						hit.SendMessage("Damage", BASE_EXPLOSION_DAMAGE);
					}
					// Hit Player
					else if (hit.tag == "Player") {
						Experience playerxp = hit.GetComponent<Experience>();
						float Damage = BASE_EXPLOSION_DAMAGE * ((float)Level / (float)playerxp.GetLevel());
						hit.SendMessage("Damage", Damage);
					}
				}
			}
		}

		// Particles
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