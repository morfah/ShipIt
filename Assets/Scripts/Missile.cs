using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public float TimeLimit = 5f;
	public GameObject Explosion;

	private float spawnedTime;
	private string[] Tags;
	private string Tag;
//	float i = 0.0f;

	// Use this for initialization
	void Start () {
		spawnedTime = Time.time;

		// constantForce seems best for missiles.
		rigidbody.constantForce.force = transform.up * 500;
	}
	
	// Update is called once per frame
	void Update () {
		// If the missile has not collided with anything for a while it will Explode()
		if ((Time.time - spawnedTime) > TimeLimit)
			Explode();
	}

	void FixedUpdate () {
//		transform.Translate(Vector3.up * (50f + i) * Time.deltaTime);
//		i = (i + 2.0f);

	}

	void OnCollisionEnter(Collision collision) {
		// Do not Explode() if the first collision is the player or enemy it self.
		// Because the missile spawns slightly inside the player.
		Tags = transform.tag.Split(',');
		Tag = collision.contacts[0].otherCollider.tag;
		if (Tag != Tags[1]){
			Explode();
			return;
		}
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
		//gameObject.SetActive(false);
	}
}
