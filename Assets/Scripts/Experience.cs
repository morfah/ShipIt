using UnityEngine;
using System.Collections;

public class Experience : MonoBehaviour {
	public long experience;
	public int level;
	public float ExpBonus;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GainExp (long exp) {
		// You gain experience
		experience += exp;
	}

	void ExpToLvl () {

	}

	void LvlUp () {
		// You level up
	}
}
