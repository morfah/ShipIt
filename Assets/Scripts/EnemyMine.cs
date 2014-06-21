using UnityEngine;
using System.Collections;

public class EnemyMine : Enemy {
	public float damageRadius = 10f;
	public float baseDamage = 100f;
	public GameObject explosion;

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
				if (distToPlayer > 4f) {
					MoveToPlayer(false);
					StatusLights(Color.red);
				}
				else
					Explode();
			}
			
		}
		else
			StatusLights(Color.green);
	}

	// Explode mine
	void Explode () {	
		//See Explosion.cs for all the Explosion logic
		GameObject expl = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
		expl.GetComponent<Explosion>().Level = Level;
		expl.GetComponent<Explosion>().Friendly = false;
		expl.GetComponent<Explosion>().damageRadius = damageRadius;
		expl.GetComponent<Explosion>().baseDamage = baseDamage;
		
		//Dissapear
		Destroy(gameObject);
	}

	void StatusLights (Color color) {
		light.color = color;
	}
}
