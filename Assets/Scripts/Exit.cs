﻿using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider collider){
		if (collider.tag == "Player") {
			Application.LoadLevel(Application.loadedLevel + 1);
		}
	}
}
