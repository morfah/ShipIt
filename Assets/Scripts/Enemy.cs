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

	private Vector3 raycastOrigin;
	private Vector3 raycastDirection;
	private RaycastHit raycastHit;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float distToPlayer = Vector3.Distance(player.transform.position, transform.position);
		int layerMask = 1 << 8;
		layerMask = ~ layerMask; // Ignore the Missile layer when raycasting

		gameObject.renderer.material.color = Color.yellow;

		raycastOrigin = transform.position;
		raycastDirection = player.transform.position - transform.position;
		raycastDirection.y = 0; 
		raycastDirection.Normalize();

		// Check if the enemy can see the Player
		if (Physics.Raycast(raycastOrigin, raycastDirection, out raycastHit, 100, layerMask)){

			Debug.DrawLine (raycastOrigin, raycastHit.point, Color.cyan, 1.0f, true);

			if (raycastHit.transform.tag == "Player") {
				if (distToPlayer > 10f) {
					MoveToPlayer();
				}
				LookAtPlayer();
			}

		}

		// Check if the enemy gun can "see" the Player
		raycastOrigin = PrimaryWeaponOrigin.transform.position;
		if (Physics.Raycast(raycastOrigin, raycastDirection, out raycastHit, 50, layerMask)){
			
			Debug.DrawLine (raycastOrigin, raycastHit.point, Color.red, 1.0f, true);

			if (raycastHit.transform.tag == "Player") {
				// Don't shoot if player is already dead
				if (!player.GetComponent<Player>().dead) {
					Shoot ();
				}
				LookAtPlayer();
			}

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

	void MoveToPlayer() {
		float step = MovementSpeed * Time.deltaTime;
		gameObject.renderer.material.color = Color.magenta;
		transform.position = Vector3.MoveTowards (transform.position, player.transform.position, step);
	}

	public int GetEnemyLevel () {
		return Level;
	}
}
