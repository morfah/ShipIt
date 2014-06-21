using UnityEngine;
using System.Collections;

public class EnemyShip : Enemy {
	public Rigidbody PrimaryWeaponType;
	public Rigidbody SecondaryWeaponType;
	public double PrimaryRefireRate = 3;
	public double SecondaryRefireRate;
	public GameObject PrimaryWeaponOrigin;
	public GameObject SecondaryWeaponOrigin;

	private double i = 0; // Used for refire loop
	private Rigidbody Projectile;

	// Update is called once per phys frame
	void FixedUpdate () {
		float distToPlayer = Vector3.Distance(player.transform.position, transform.position);
		int layerMask = 1 << 8;
		layerMask = ~ layerMask; // Ignore the Missile layer when raycasting
		
		gameObject.renderer.material.color = Color.yellow;
		
		raycastOrigin = transform.position;
		raycastDirection = player.transform.position - transform.position;
		//raycastDirection.y = player.GetComponent<Player>().StartYPosition - transform.position.y; 
		raycastDirection.Normalize();
		
		// Check if the enemy can see the Player
		if (Physics.Raycast(raycastOrigin, raycastDirection, out raycastHit, 100, layerMask)){
			
			Debug.DrawLine (raycastOrigin, raycastHit.point, Color.cyan, 1.0f, true);
			
			if (raycastHit.transform.tag == "Player") {
				if (distToPlayer > 10f)
					MoveToPlayer(false);
				else if (distToPlayer < 7f)
					MoveToPlayer(true); // Backs away from player
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
}
