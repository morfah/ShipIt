using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class ApplyDamage : MonoBehaviour {
	public int HealthPoints = 100;
	public int ArmorPoints = 0;
	public GameObject Explosion;
//	public AudioClip HitConfirmedSound;

	private int BaseMissileDamage = 25;
	private int BaseBulletDamage = 10;
	private string[] Tags;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (HealthPoints <= 0)
			Die();
	}

	void OnCollisionEnter(Collision collision) {
		//if (contactpoint.otherCollider.tag.Contains(",")) {
		if (collision.contacts[0].otherCollider.tag.Contains(",")) {
			//Tags = contactpoint.otherCollider.tag.Split(',');
			Tags = collision.contacts[0].otherCollider.tag.Split(',');
			// Do not apply damage if colliding with an child object. Like a missile the player or enemy fired it self.
			if (transform.tag != Tags[1]) {
				// Damage types
				// TODO Armor code
				switch (Tags[0]) {
				case "Missile":
					//Debug.Log("Missile hit!");
					HealthPoints -= BaseMissileDamage;
					//                if (contactpoint.thisCollider.tag == "Enemy" && contactpoint.thisCollider.tag != "Player")
					//                    audio.PlayOneShot(HitConfirmedSound, 0.7F);
					break;
				case "Bullet":
					//Debug.Log("Bullet hit!");
					HealthPoints -= BaseBulletDamage;
					break;
				}
			}
		}
		else{
			// Other apply damage code here. e.g environmental
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
