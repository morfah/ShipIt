using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Rigidbody testBullet;
	public GameObject bulletOrigin;
	public float MovementSpeed;
	public float RotateSpeed;
	public double RefireRate;

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
		if (RotateSpeed == 0f)
			RotateSpeed = 10f;
		if (RefireRate == 0)
		    RefireRate = 8; // default firerate, 8 shots per second
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
		bool FlyUp = Input.GetButton("FlyUp");
		bool FlyDown = Input.GetButton("FlyDown");
		bool Boost = Input.GetButton("Boost");

		MovementSpeedBonus = Boost ? 2f : 1f;

		transform.Translate(Vector3.right * h * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		transform.Translate(Vector3.forward * v * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		//transform.Translate(Vector3.up * FlyUp * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		//transform.Translate(Vector3.down * FlyDown * (MovementSpeed * MovementSpeedBonus) * Time.deltaTime);
		transform.Rotate(Vector3.up * mouseX * RotateSpeed * Time.deltaTime);

		// Fire update
		if (Fire1 && i >= (1 / RefireRate))
		{
			Instantiate(testBullet, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
			i = 0;
		}
	        else if (i > RefireRate)
	            i = 0;
		i += Time.deltaTime;
	}
}
