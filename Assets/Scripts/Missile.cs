using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public float TimeLimit = 5f;
	public GameObject Explosion;
	[HideInInspector]
	public int Level = 1;
	[HideInInspector]
	public bool Friendly = true;

	private float spawnedTime;
	private string[] Tags;
	private string Tag;

	// Use this for initialization
	void Start () {
		spawnedTime = Time.time;

		// constantForce seems best for missiles.
		rigidbody.constantForce.force = transform.up * 500;

		// Color
		if (Friendly)
			gameObject.renderer.material.color = Color.green;
		else
			gameObject.renderer.material.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
		// If the missile has not collided with anything for a while it will Explode()
		if ((Time.time - spawnedTime) > TimeLimit) {
			Explode ();
		}
	}

	void FixedUpdate () {

	}

	void OnCollisionEnter(Collision collision) {
			Explode ();		
	}

	// Explode missile
	void Explode()
	{
		// Only destroy the clone objects. Don't touch the original.
		// TODO: Is there a better way to spawn objects than to Instantiate() an existed GameObject?
		if (!gameObject.name.EndsWith("(Clone)")) 
			return;

		//See Explosion.cs for all the Explosion logic
		GameObject explosion = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
		explosion.GetComponent<Explosion>().Level = Level;
		explosion.GetComponent<Explosion>().Friendly = Friendly;

		//Dissapear
		Destroy(gameObject);
	}
}
