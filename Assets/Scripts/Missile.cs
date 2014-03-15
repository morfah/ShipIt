using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {
	public float TimeLimit;
	public GameObject Explosion;

	private float spawnedTime;
	private string[] Tags;
	private string Tag;

	// Use this for initialization
	void Start () {
		//Default variables.
		if (TimeLimit == 0f)
			TimeLimit = 5f;

		spawnedTime = Time.time;

		// constantForce seems best for missiles.
		rigidbody.constantForce.force = transform.up * 500;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate(Vector3.up * 40 * Time.deltaTime);

		// If the missile has not collided with anything for a while it will Explode()
		if ((Time.time - spawnedTime) > TimeLimit)
			Explode();
	}

	void OnCollisionEnter(Collision collision) {
		// Do not Explode() if the first collision is the player or enemy it self.
		// Because the missile spawns slightly inside the player.
		Tags = this.tag.Split(',');
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
