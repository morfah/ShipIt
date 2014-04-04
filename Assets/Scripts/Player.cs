using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Rigidbody PrimaryWeaponType;
	public Rigidbody SecondaryWeaponType;
	public double PrimaryRefireRate = 8;
	public double SecondaryRefireRate;
	public GameObject PrimaryWeaponOrigin;
	public GameObject SecondaryWeaponOrigin;

	public float MovementSpeed = 10f;
	public float MouseSensitivity = 10f;
	public float SpeedBoostMultiplier = 2f;

	private double i = 0;
	private Rigidbody bulletInstance;
	private float missileStartTime;
	private float timer;
	private float MovementSpeedBonus;
	private Rigidbody Projectile;

	private float h;
	private float v;
	private float mouseX;
	private bool Fire1;
	private bool Boost;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true; // so that the mouse wont escape the window
	}
	
	// Update is called once per frame
	void Update () {
		// Fire1 update
		if (Fire1 && i >= (1 / PrimaryRefireRate))
		{
			Projectile = Instantiate(PrimaryWeaponType,
			            PrimaryWeaponOrigin.transform.position, 
			            PrimaryWeaponOrigin.transform.rotation) as Rigidbody;
			Projectile.tag = PrimaryWeaponType.tag + ",Player";
			i = 0;
		}
	        else if (i > PrimaryRefireRate)
	            i = 0;
		i += Time.deltaTime;

		//TODO add Fire2 code
	}

	void FixedUpdate () {
		// Movement update
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		mouseX = Input.GetAxis("Mouse X");
		Fire1 = Input.GetButton("Fire1");
		//bool FlyUp = Input.GetButton("FlyUp");
		//bool FlyDown = Input.GetButton("FlyDown");
		Boost = Input.GetButton("Boost");
		
		MovementSpeedBonus = Boost ? SpeedBoostMultiplier : 1f;
		
		transform.Translate(Vector3.right * h * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		transform.Translate(Vector3.forward * v * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		//transform.Translate(Vector3.up * FlyUp * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		//transform.Translate(Vector3.down * FlyDown * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		transform.Rotate(Vector3.up * mouseX * MouseSensitivity * Time.deltaTime);
	}

	void OnGUI () {
		int hp;
		int ap;
		ApplyDamage[] ad;
		ad = gameObject.GetComponents<ApplyDamage>();
		hp = ad[0].HealthPoints;
		ap = ad[0].ArmorPoints;
		
		GUI.Box(new Rect(Screen.width / 5, Screen.height - 30, 300, 25), 
		          "HP: " + hp + "  Armor: " + ap + "  Exp: soon  Lvl: soon");

		if (hp <= 0) {
			GUI.Box(new Rect(Screen.width / 2-50, 50, 100, 20), 
			        "\"Dead\"");
		}
	}
}
