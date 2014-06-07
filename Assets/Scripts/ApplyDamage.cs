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
	private const float BASE_ENEMY_XP_REWARD = 75.0f;
	private const float BASE_CRATE_SMALL_XP_REWARD = 25.0f;
	private const float BASE_CRATE_LARGE_XP_REWARD = 50.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (HealthPoints <= 0) {
			Die ();
		}
	}

	void Damage(float dmg) {
		HealthPoints -= (int)dmg;
	}
	
	void Die() {
		if (tag != "Player"){
			GameObject exppoint;
			Instantiate(Explosion, transform.position, transform.rotation);

			// Give Experience
			if (tag == "Enemy"){
				Enemy enemy = gameObject.GetComponent<Enemy>();
				Experience playerxp = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
				// Give experience reward based on player and enemy level
				float ExperienceReward = BASE_ENEMY_XP_REWARD * ((float)enemy.GetEnemyLevel() / (float)playerxp.GetLevel());
				GameObject.FindGameObjectWithTag("Player").SendMessage("GainExp", ExperienceReward);
			}
			else if (tag == "SmallLootCrate"){

				for (int i = 0; i < BASE_CRATE_SMALL_XP_REWARD; i++){
					exppoint = Instantiate(VisualExpPoint, 
					                       transform.position,
					                       transform.rotation) as GameObject;
					exppoint.tag = "ExpPoint";
					exppoint.AddComponent("VisualExperiencePoint");
				}

				GameObject.FindGameObjectWithTag("Player").SendMessage("GainExp", BASE_CRATE_SMALL_XP_REWARD);
			}
			else if (tag == "LargeLootCrate"){
				
				for (int i = 0; i < BASE_CRATE_LARGE_XP_REWARD; i++){
					exppoint = Instantiate(VisualExpPoint, 
					                       transform.position,
					                       transform.rotation) as GameObject;
					exppoint.tag = "ExpPoint";
					exppoint.AddComponent("VisualExperiencePoint");
				}
				
				GameObject.FindGameObjectWithTag("Player").SendMessage("GainExp", BASE_CRATE_LARGE_XP_REWARD);
			}

			Destroy(gameObject); // remove the object that died
		}
		else{
			// Player death code here
		}
	}
}
