using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class ApplyDamage : MonoBehaviour {
	public int HealthPoints;
	public int ArmorPoints;
	public GameObject Explosion;
//	public AudioClip HitConfirmedSound;

	private int BaseMissileDamage;
	private int BaseBulletDamage;
	private string[] Tags;

	// Use this for initialization
	void Start () {
		//Default variables
		if (HealthPoints == 0)
			HealthPoints = 100;

		if (ArmorPoints == 0)
			ArmorPoints = 0;

		BaseMissileDamage = 25;
		BaseBulletDamage = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if (HealthPoints <= 0)
			Die();
	}

	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contactpoint in collision.contacts){
			Tags = contactpoint.otherCollider.tag.Split(',');
			// Do not apply damage if colliding with an child object. Like a missile the player or enemy fired it self.
			if (this.tag != Tags[1]) {
				// Damage types
				// TODO Armor code
				switch (Tags[0]) {
				case "Missile":
					//Debug.Log("Missile hit!");
					HealthPoints -= BaseMissileDamage;
	//				if (contactpoint.thisCollider.tag == "Enemy" && contactpoint.thisCollider.tag != "Player")
	//					audio.PlayOneShot(HitConfirmedSound, 0.7F);
					break;
				case "Bullet":
					//Debug.Log("Bullet hit!");
					HealthPoints -= BaseBulletDamage;
					break;
				default:
					break;
				}
			}
		}
	}
	
	void Die() {
		if (tag != "Player"){
			Instantiate(Explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
		else {
			Debug.Log("YOU DIED! GAME OVER! STOP PLAYING!");
		}

	}
}
