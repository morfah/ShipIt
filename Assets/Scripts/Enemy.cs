using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Rigidbody PrimaryWeaponType;
	public Rigidbody SecondaryWeaponType;
	public double PrimaryRefireRate = 3;
	public double SecondaryRefireRate;
	public GameObject PrimaryWeaponOrigin;
	public GameObject SecondaryWeaponOrigin;

	public int MovementSpeed = 10;
	public float TurnSpeed = 5f;

	public int Level = 1;

	private double i = 0; 
	private Rigidbody Projectile;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float step = MovementSpeed * Time.deltaTime;
		float distToPlayer = Vector3.Distance(player.transform.position, transform.position);
		//Vector3 pos = player.transform.position;
		//Quaternion rot = Quaternion.LookRotation(pos);
		gameObject.renderer.material.color = Color.yellow;
		if (distToPlayer < 45f && distToPlayer > 10f) {
			gameObject.renderer.material.color = Color.magenta;
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, step);
			LookAtPlayer();
		}
	    
		if (distToPlayer < 30f) {
			//TODO aim prediction... ugh...
			Shoot ();
			LookAtPlayer();
		}
	}

	void Shoot() {
		gameObject.renderer.material.color = Color.red;
		if (i >= (1 / PrimaryRefireRate))
		{
			Projectile = Instantiate(PrimaryWeaponType,
			            PrimaryWeaponOrigin.transform.position, 
			            PrimaryWeaponOrigin.transform.rotation) as Rigidbody;
			Projectile.tag = PrimaryWeaponType.tag;
			Projectile.GetComponent<Missile>().Friendly = false;
			Projectile.GetComponent<Missile>().Level = Level;
			i = 0;
		}
		else if (i > PrimaryRefireRate)
			i = 0;
		i += Time.deltaTime;

		//TODO maybe secondary fire too?
	}

	void LookAtPlayer(){
		Vector3 moveDirection = player.transform.position - transform.position;
		moveDirection.y = 0; 
		moveDirection.Normalize();

		float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
		transform.rotation = 
			Quaternion.Slerp(transform.rotation, 
			                 Quaternion.Euler(0, targetAngle, 0), 
			                 TurnSpeed * Time.deltaTime);
	}

	public int GetEnemyLevel () {
		return Level;
	}
}
