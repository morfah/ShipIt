using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Rigidbody PrimaryWeaponType;
	public Rigidbody SecondaryWeaponType;
	public double PrimaryRefireRate;
	public double SecondaryRefireRate;
	public GameObject PrimaryWeaponOrigin;
	public GameObject SecondaryWeaponOrigin;

	public int MovementSpeed;

	private double i; 
	private Rigidbody Projectile;

	// Use this for initialization
	void Start () {
		// Default variables
		if (MovementSpeed == 0)
			MovementSpeed = 10;
		if (PrimaryRefireRate == 0)
			PrimaryRefireRate = 3;
		i = 0;
	
		this.tag = "Enemy";
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindWithTag("Player");
		float step = MovementSpeed * Time.deltaTime;
		float distToPlayer = Vector3.Distance(player.transform.position, transform.position);
		//Vector3 pos = player.transform.position;
		//Quaternion rot = Quaternion.LookRotation(pos);

		if (distToPlayer < 45f && distToPlayer > 10f) {
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
			transform.LookAt(player.transform.position);
			//transform.rotation = Quaternion.Lerp(transform.rotation, rot, Speed); 
		}

		if (distToPlayer < 30f) {
			//TODO aim prediction... ugh...
			Shoot();
			transform.LookAt(player.transform.position);

		}
	}

	void Shoot() {
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
