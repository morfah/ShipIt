﻿using UnityEngine;
using System.Collections;

public class Buoy : MonoBehaviour {
	GameObject player;
	ApplyDamage ad;
	bool BuoyActive = false;
	int HealthPool = 100;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		ad = player.GetComponent<ApplyDamage>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		if (HealthPool < 1)
			return;

		if (BuoyActive) {
			ad.HealthPoints++;
			HealthPool--;
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
