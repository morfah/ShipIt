using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Rigidbody PrimaryWeaponType;
	public Rigidbody SecondaryWeaponType;
	public double PrimaryRefireRate;
	public double SecondaryRefireRate;
	public GameObject PrimaryWeaponOrigin;
	public GameObject SecondaryWeaponOrigin;

	public float MovementSpeed;
	public float MouseSensitivity;

	private double i;
	private Rigidbody bulletInstance;
	private float missileStartTime;
	private float timer;
	private float MovementSpeedBonus;

	// Use this for initialization
	void Start () {
		//Default variables.
		if (MovementSpeed == 0f)
			MovementSpeed = 10f;
		if (MouseSensitivity == 0f)
			MouseSensitivity = 10f;
		if (PrimaryRefireRate == 0)
		    PrimaryRefireRate = 8; // default firerate, 8 shots per second
		Screen.lockCursor = true; // so that the mouse wont escape the window
		i = 0;
		this.tag = "Player";
	}
	
	// Update is called once per frame
	void Update () {
		// Movement update
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		float mouseX = Input.GetAxis("Mouse X");
		bool Fire1 = Input.GetButton("Fire1");
		//bool FlyUp = Input.GetButton("FlyUp");
		//bool FlyDown = Input.GetButton("FlyDown");
		bool Boost = Input.GetButton("Boost");

		MovementSpeedBonus = Boost ? 2f : 1f;

		transform.Translate(Vector3.right * h * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		transform.Translate(Vector3.forward * v * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		//transform.Translate(Vector3.up * FlyUp * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		//transform.Translate(Vector3.down * FlyDown * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		transform.Rotate(Vector3.up * mouseX * MouseSensitivity * Time.deltaTime);

		// Fire1 update
		if (Fire1 && i >= (1 / PrimaryRefireRate))
		{
			Instantiate(PrimaryWeaponType,
			            PrimaryWeaponOrigin.transform.position, 
			            PrimaryWeaponOrigin.transform.rotation);
			i = 0;
		}
	        else if (i > PrimaryRefireRate)
	            i = 0;
		i += Time.deltaTime;

		//TODO add Fire2 code
	}
}
