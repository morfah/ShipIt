using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int HealthPoints;
	public int ArmorPoints;
	public int Speed;

	// Use this for initialization
	void Start () {
		// Default variables
		if (HealthPoints == 0)
			HealthPoints = 100;
		if (ArmorPoints == 0)
			ArmorPoints = 0;
		this.tag = "Enemy";
	}
	
	// Update is called once per frame
	void Update () {
		if (HealthPoints <= 0)
			Die();
	}

	void Die () {
		Destroy(gameObject);
	}
}
