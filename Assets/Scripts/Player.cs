using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	[HideInInspector]
	public bool dead = false;

	public Rigidbody PrimaryWeaponType;
	public Rigidbody SecondaryWeaponType;
	public double PrimaryRefireRate = 8;
	public double SecondaryRefireRate = 2;
	public GameObject PrimaryWeaponOrigin;
	public GameObject SecondaryWeaponOrigin;

	public float MovementSpeed = 10f;
	public float MouseSensitivity = 10f;
	public float SpeedBoostMultiplier = 2f;
	private Vector3 MoveDirection = Vector3.zero;
	private float StartYPosition;
	public float pushPower = 150.0f;

	private double i1 = 0;
//	private double i2 = 0;
	private Rigidbody bulletInstance;
	private float missileStartTime;
	private float timer;
	private float MovementSpeedBonus;
	private Rigidbody Projectile;
//	private Rigidbody Projectile2;

	private float h;
	private float v;
	private float mouseX;
	private bool Fire1;
//	private bool Fire2;
	private bool Boost;
	private float look;
	private bool ToggleCamera;
	private Transform cam;

//	private Vector3 HitscanOrigin;
//	private Vector3 HitscanDirection;
//	private RaycastHit HitscanHit;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true; // so that the mouse wont escape the window
		cam = Camera.main.camera.transform;
		StartYPosition = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if (!dead) {
			// Fire1 update
			if (Fire1 && i1 >= (1 / PrimaryRefireRate))
			{
				Projectile = Instantiate(PrimaryWeaponType,
				            PrimaryWeaponOrigin.transform.position, 
				            PrimaryWeaponOrigin.transform.rotation) as Rigidbody;
				Projectile.tag = PrimaryWeaponType.tag;
				Projectile.GetComponent<Missile>().Friendly = true;
				Projectile.GetComponent<Missile>().Level = gameObject.GetComponent<Experience>().GetLevel();
				i1 = 0;
			}
		        else if (i1 > PrimaryRefireRate)
		            i1 = 0;
			i1 += Time.deltaTime;

			// Fire2 update
	//		if (Fire1 && i2 >= (1 / SecondaryRefireRate))
	//		{
	//			HitscanDirection = transform.TransformDirection(Vector3.forward);
	//			HitscanOrigin = SecondaryWeaponOrigin.transform.position;
	//
	//			if (Physics.Raycast(HitscanOrigin, HitscanDirection, out HitscanHit, 300.0f)){
	//				Debug.DrawLine (HitscanOrigin, HitscanHit.point, Color.cyan, 1.0f, true);
	//			}
	//			i2 = 0;
	//		}
	//		else if (i2 > SecondaryRefireRate)
	//			i2 = 0;
	//			i2 += Time.deltaTime;

			// Change camera angle if wanted
			if (ToggleCamera) {
				ToggleCameraAngle();
			}
		}
	}

	void FixedUpdate () {
		if (!dead) {

			// Movement update
			CharacterController controller = GetComponent<CharacterController>();
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");
			mouseX = Input.GetAxis("Mouse X");
			look = Input.GetAxis ("Look");
			Fire1 = Input.GetButton("Fire1");
	//		Fire2 = Input.GetButton("Fire2");
			//bool FlyUp = Input.GetButton("FlyUp");
			//bool FlyDown = Input.GetButton("FlyDown");
			Boost = Input.GetButton("Boost");
			ToggleCamera = Input.GetButtonUp ("ToggleCameraMode");
			
			MovementSpeedBonus = Boost ? SpeedBoostMultiplier : 1f;

			if (Boost)
				Fire1 = false; // you can't boost n' shoot

			MoveDirection = new Vector3(h, StartYPosition - transform.position.y , v);
			MoveDirection = transform.TransformDirection(MoveDirection);
			MoveDirection *= MovementSpeed * MovementSpeedBonus;

			controller.Move(MoveDirection * Time.deltaTime);

//			rigidbody.AddForce(MoveDirection.x, MoveDirection.y, MoveDirection.z, ForceMode.Acceleration);


//			transform.Translate(Vector3.right * h * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
//			transform.Translate(Vector3.forward * v * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
			//transform.Translate(Vector3.up * FlyUp * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
			//transform.Translate(Vector3.down * FlyDown * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
			transform.Rotate(Vector3.up * mouseX * MouseSensitivity * Time.deltaTime);
			transform.Rotate (Vector3.up * look * (MouseSensitivity * 1.5f) * Time.deltaTime);


		}
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic || body.tag == "Missile" || body.tag == "Player" || body.tag == "Enemy")
			return;

		Vector3 pushDir = new Vector3(hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z);
		body.velocity = pushDir * (pushPower / body.mass);
	}
	
	void ToggleCameraAngle(){
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
