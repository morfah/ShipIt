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
		GameObject player = GameObject.FindWithTag("Player");
		float step = Speed * Time.deltaTime;
		float distToPlayer = Vector3.Distance(player.transform.position, transform.position);

		if (distToPlayer < 45f && distToPlayer > 5f) {
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
			transform.LookAt(player.transform.position);
		}
	}
}
