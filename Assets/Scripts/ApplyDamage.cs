using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class ApplyDamage : MonoBehaviour {
	public int HealthPoints = 100;
	public int ArmorPoints = 0;
	public GameObject Explosion;
	public GameObject VisualExpPoint;

	private const float BASE_ENEMY_XP_REWARD = 75.0f;
	private const float BASE_CRATE_SMALL_XP_REWARD = 25.0f;
	private const float BASE_CRATE_LARGE_XP_REWARD = 50.0f;

	private Experience player;
	private Enemy enemy;

	// Use this for initialization
	void Start () {
		enemy = gameObject.GetComponent<Enemy>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
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

		if (tag != "Player") {
			GameObject exppoint;
			Instantiate(Explosion, transform.position, transform.rotation);

			// Give Experience
			if (tag == "Enemy") {

				// Give experience reward based on player and enemy level
				float ExperienceReward = BASE_ENEMY_XP_REWARD * ((float)enemy.GetEnemyLevel() / (float)player.GetLevel());
				player.GainExp(ExperienceReward);

			}
			else if (tag == "SmallLootCrate") {

				// Spawn lots of experience sprites
				for (int i = 0; i < BASE_CRATE_SMALL_XP_REWARD; i++){
					exppoint = Instantiate(VisualExpPoint, 
					                       transform.position,
					                       transform.rotation) as GameObject;
					exppoint.tag = "ExpPoint";
					exppoint.AddComponent("VisualExperiencePoint");
				}

				player.GainExp(BASE_CRATE_SMALL_XP_REWARD);
			}
			else if (tag == "LargeLootCrate") {

				//Spawn lots of experience sprites
				for (int i = 0; i < BASE_CRATE_LARGE_XP_REWARD; i++){
					exppoint = Instantiate(VisualExpPoint, 
					                       transform.position,
					                       transform.rotation) as GameObject;
					exppoint.tag = "ExpPoint";
					exppoint.AddComponent("VisualExperiencePoint");
				}

				player.GainExp(BASE_CRATE_LARGE_XP_REWARD);
			}

			Destroy(gameObject); // remove the object that died
		}
		else{
			// Player death code here.
			if (!player.GetComponent<Player>().dead) {
				Instantiate(Explosion, transform.position, transform.rotation);
				player.renderer.enabled = false;
			}
		}
	}
}
