using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public float TimeLimit;
	public GameObject Explosion;

	private float spawnedTime;
	//private GameObject nme;

	// Use this for initialization
	void Start () {
		//Default variables.
		if (TimeLimit == 0f)
			TimeLimit = 5f;
//		if (Damage == 0f)
//			Damage = 25f;

		spawnedTime = Time.time;

		// constantForce seems best for missiles.
		rigidbody.constantForce.force = transform.up * 500;

		this.tag = "Missile";
	}
	
	// Update is called once per frame
	void Update () {
		// If the missile has not collided with anything for a while it will Explode()
		if ((Time.time - spawnedTime) > TimeLimit)
			Explode();
	}

	void OnCollisionEnter(Collision collision) {
		// Do not Explode() if the first collision is the player it self.
		// Because the missile spawns slightly inside the player.
		//if (collision.contacts[0].otherCollider.tag != "Player"){
		if (!transform.IsChildOf(collision.contacts[0].otherCollider.transform)) {
			Explode();
			return;
		}

//		foreach (ContactPoint contactpoint in collision.contacts){
//			if (contactpoint.otherCollider.tag == "Enemy") {
////				nme = GameObject.Find("Enemy").GetComponent(BehaviorScript);
////				nme.HealthPoints += Damage;
//				Enemy nme;
//				nme = contactpoint.otherCollider.GetComponent(Enemy);
//				nme.HealthPoints += Damage;
//			}
//			else if (contactpoint.otherCollider.tag != "Player"){
//				Explode ();
//			}
//		}
	}

	// Explode missile
	void Explode()
	{
		// Only destroy the clone objects. Don't touch the original.
		// TODO: Is there a better way to spawn objects than to Instantiate() an existed GameObject?
		if (!gameObject.name.EndsWith("(Clone)")) 
			return;

		//See Explosion.cs for all the Explosion logic
		Instantiate(Explosion, transform.position, transform.rotation);

		//Dissapear
		//transform.DetachChildren();
		Destroy(gameObject);
	}
}
