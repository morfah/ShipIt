using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class ApplyDamage : MonoBehaviour {
	public int HealthPoints = 100;
	public int ArmorPoints = 0;
	public GameObject Explosion;
//	public AudioClip HitConfirmedSound;

//	private int BaseMissileDamage = 25;
//	private int BaseBulletDamage = 10;
	private string[] Tags;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (HealthPoints <= 0) {
			Die ();
		}
	}

	void Damage(int dmg) {
		HealthPoints -= dmg;
	}
	
	void Die() {
		if (tag != "Player"){
			Instantiate(Explosion, transform.position, transform.rotation);
			Destroy(gameObject);
		}
		else {
			//Debug.Log("YOU DIED! GAME OVER! STOP PLAYING!");
		}

	}
}
