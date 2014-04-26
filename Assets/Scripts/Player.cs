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
	private float look;
	private bool ToggleCamera;
	private Transform cam;
	private ApplyDamage ad;
	private Experience ex;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true; // so that the mouse wont escape the window
		cam = Camera.main.camera.transform;
		ad = gameObject.GetComponent<ApplyDamage>();
		ex = gameObject.GetComponent<Experience>();
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

		if (ToggleCamera) {
			ToggleCameraAngle();
		}


//		GameObject.FindGameObjectsWithTag("ExpPoint").
//		exppoint.transform.position(GameObject.FindGameObjectWithTag("Player"));
	}

	void FixedUpdate () {
		// Movement update
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		mouseX = Input.GetAxis("Mouse X");
		look = Input.GetAxis ("Look");
		Fire1 = Input.GetButton("Fire1");
		//bool FlyUp = Input.GetButton("FlyUp");
		//bool FlyDown = Input.GetButton("FlyDown");
		Boost = Input.GetButton("Boost");
		ToggleCamera = Input.GetButtonUp ("ToggleCameraMode");
		
		MovementSpeedBonus = Boost ? SpeedBoostMultiplier : 1f;
		
		transform.Translate(Vector3.right * h * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		transform.Translate(Vector3.forward * v * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		//transform.Translate(Vector3.up * FlyUp * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		//transform.Translate(Vector3.down * FlyDown * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		transform.Rotate(Vector3.up * mouseX * MouseSensitivity * Time.deltaTime);
		transform.Rotate (Vector3.up * look * (MouseSensitivity * 1.5f) * Time.deltaTime);
	}

	void OnGUI () {
		GUI.Box(new Rect(Screen.width / 5, Screen.height - 30, 150, 25), 
		        "HP: " + ad.HealthPoints + "  Armor: " + ad.ArmorPoints);

		GUI.Box(new Rect(Screen.width / 2, Screen.height - 30, 200, 25), 
		        ex.experience + " XP  Level " + ex.level);
		
		if (ad.HealthPoints <= 0) {
			GUI.Box(new Rect(Screen.width / 2-50, 50, 100, 20), 
			        "You Died");
		}
	}

	void ToggleCameraAngle(){
//		Debug.Log (cam.localPosition);
//		Debug.Log (cam.localEulerAngles);
//		Debug.Log (cam.localScale);

//		(0.0, 8.4, -6.5)
//		(24.7, 0.0, 0.0)
//		(1.0, 1.0, 1.0)

		if (cam.localPosition != new Vector3(0.0f, 50.0f, 15.0f)) {
			cam.localPosition = new Vector3 (0.0f, 50.0f, 15.0f);
			cam.localEulerAngles = new Vector3 (80.0f, 0.0f, 0.0f);
			cam.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			Debug.Log("TOP-DOWN VIEW");
		} else {
			cam.localPosition = new Vector3 (0.0f, 8.4f, -6.5f);
			cam.localEulerAngles = new Vector3 (24.7f, 0.0f, 0.0f);
			cam.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			Debug.Log ("TPS VIEW");
		}
	}
}
