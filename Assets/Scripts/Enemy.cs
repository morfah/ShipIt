using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int Speed;

	// Use this for initialization
	void Start () {
		// Default variables
		if (Speed == 0)
			Speed = 10;

		this.tag = "Enemy";
	}
	
	// Update is called once per frame
	void Update () {

	}
}
