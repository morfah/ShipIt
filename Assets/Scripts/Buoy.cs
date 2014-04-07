using UnityEngine;
using System.Collections;

public class Buoy : MonoBehaviour {
	GameObject player;

	// Use this for initialization
	void Start () {
//		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider collider){
		if (collider.tag == "Player"){
			Debug.Log ("Player went inside the Buoy trigger!");
		}
	}

	void OnTriggerExit (Collider collider){
		if (collider.tag == "Player"){
			Debug.Log ("Player went outside the Buoy trigger!");
		}
	}
}
