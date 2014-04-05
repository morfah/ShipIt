﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Rigidbody PrimaryWeaponType;
	public Rigidbody SecondaryWeaponType;
	public double PrimaryRefireRate = 3;
	public double SecondaryRefireRate;
	public GameObject PrimaryWeaponOrigin;
	public GameObject SecondaryWeaponOrigin;

	public int MovementSpeed = 10;

	private double i = 0; 
	private Rigidbody Projectile;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GameObject player = GameObject.FindWithTag("Player");
		float step = MovementSpeed * Time.deltaTime;
		float distToPlayer = Vector3.Distance(player.transform.position, transform.position);
		//Vector3 pos = player.transform.position;
		//Quaternion rot = Quaternion.LookRotation(pos);
		gameObject.renderer.material.color = Color.yellow;
		if (distToPlayer < 45f && distToPlayer > 10f) {
			gameObject.renderer.material.color = Color.magenta;
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, step);
			transform.LookAt (player.transform.position);
			//transform.rotation = Quaternion.Lerp(transform.rotation, rot, Speed); 
		}
	    
		if (distToPlayer < 30f) {
			//TODO aim prediction... ugh...
			Shoot ();
			transform.LookAt (player.transform.position);
		}
	}

	void Shoot() {
		gameObject.renderer.material.color = Color.red;
		if (i >= (1 / PrimaryRefireRate))
		{
			Projectile = Instantiate(PrimaryWeaponType,
			            PrimaryWeaponOrigin.transform.position, 
			            PrimaryWeaponOrigin.transform.rotation) as Rigidbody;
			Projectile.tag = PrimaryWeaponType.tag + ",Enemy";
			i = 0;
		}
		else if (i > PrimaryRefireRate)
			i = 0;
		i += Time.deltaTime;

		//TODO maybe secondary fire too?
	}
}
