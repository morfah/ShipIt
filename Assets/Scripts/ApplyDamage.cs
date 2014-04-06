using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class ApplyDamage : MonoBehaviour {
	public int HealthPoints = 100;
	public int ArmorPoints = 0;
	public GameObject Explosion;
	public GameObject VisualExpPoint;
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
			GameObject exppoint;
			Instantiate(Explosion, transform.position, transform.rotation);
			Destroy(gameObject);
			if (tag == "Enemy"){

				for (int i = 0; i < 100; i++){
					exppoint = Instantiate(VisualExpPoint, 
					                       transform.position,
					                       transform.rotation) as GameObject;
					exppoint.tag = "ExpPoint";
					exppoint.AddComponent("VisualExperiencePoint");
				}

				GameObject.FindGameObjectWithTag("Player").SendMessage("GainExp", 100);
			}
			else if (tag == "LootCrate"){

				for (int i = 0; i < 50; i++){
					exppoint = Instantiate(VisualExpPoint, 
					                       transform.position,
					                       transform.rotation) as GameObject;
					exppoint.tag = "ExpPoint";
					exppoint.AddComponent("VisualExperiencePoint");
				}

				GameObject.FindGameObjectWithTag("Player").SendMessage("GainExp", 50);
			}
		}
		else {
			//Debug.Log("YOU DIED! GAME OVER! STOP PLAYING!");
		}

	}
}
