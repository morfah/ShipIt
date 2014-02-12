using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public float TimeLimit;
	public GameObject Explosion;

	private float spawnedTime;

	// Use this for initialization
	void Start () {
		spawnedTime = Time.time;
		//Debug.Log("Missile Spawned! " + gameObject.name);

		// constantForce
		rigidbody.constantForce.force = transform.up * 500;
		//rigidbody.constantForce.relativeForce = transform.up * 1000;

		//Add force
		//rigidbody.AddForce(transform.up * 2500);

		// lek
		//rigidbody.constantForce.force = Vector3.up * 10000000;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Time.deltaTime - startTime = " + (Time.time - spawnedTime), gameObject);
		if ((Time.time - spawnedTime) > 5)
			Explode();
	}

	void OnCollisionEnter(Collision collision) {
		//Debug.Log(collision.contacts[0].otherCollider.name);
		if (collision.contacts[0].otherCollider.name != "Player")
			Explode();
	}

	// Explode missile
	void Explode()
	{
		// Only destroy the clone objects. Don't touch the original.
		// Is there a better way to do this?
		if (!gameObject.name.EndsWith("(Clone)")) 
			return;

		//Spawn light
		Instantiate(Explosion, transform.position, transform.rotation);

		//Dissapear
		Destroy(gameObject);
	}
}
