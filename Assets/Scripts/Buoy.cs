using UnityEngine;
using System.Collections;

public class Buoy : MonoBehaviour {
	GameObject player;
	ApplyDamage hp;
	bool BuoyActive = false;
	int HealthPool = 100;
	int ArmorPool = 100;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		hp = player.GetComponent<ApplyDamage>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		if (HealthPool < 1)
			return;

		if (BuoyActive) {

			// Repair hull up to 100%
			if (hp.HealthPoints < 100) {
				hp.HealthPoints++;
				HealthPool--;
			}

			// Recharge shield up to 100%
			if (hp.ArmorPoints < 100) {
				hp.ArmorPoints++;
				ArmorPool--;
			}

		}
	}

	void OnTriggerEnter (Collider collider){
		if (collider.tag == "Player") {
			BuoyActive = true;
			PlayerPrefs.Save(); // Save
			Debug.Log ("Player went inside the Buoy trigger!");
		}
	}

	void OnTriggerExit (Collider collider){
		if (collider.tag == "Player"){
			BuoyActive = false;
			PlayerPrefs.Save(); // Save
			Debug.Log ("Player went outside the Buoy trigger!");
		}
	}
}
