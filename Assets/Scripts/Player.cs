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
	

	// Use this for initialization
	void Start () {
		if (MovementSpeed == 0f)
			MovementSpeed = 10f;
		if (RotateSpeed == 0f)
			RotateSpeed = 10f;
		if (RefireRate == 0)
		    RefireRate = 8; // default firerate, 8 shots per second
		Screen.lockCursor = true; // so that the mouse wont escape the window
		i = 0;
	}
	
	// Update is called once per frame
	void Update () {
		// Movement update
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		float mouseX = Input.GetAxis("Mouse X");
		bool Fire1 = Input.GetButton("Fire1");
		transform.Translate(Vector3.right * h * MovementSpeed * Time.deltaTime);
		transform.Translate(Vector3.forward * v * MovementSpeed * Time.deltaTime);
		transform.Rotate(Vector3.up * mouseX * RotateSpeed * Time.deltaTime);

		// Fire update
		if (Fire1 && i >= (1 / RefireRate))
		{
			//bulletInstance = Instantiate(testBullet, bulletOrigin.transform.position, bulletOrigin.transform.rotation) as Rigidbody;
			Instantiate(testBullet, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
			//missileStartTime = Time.time;
			//bulletInstance.AddForce(bulletInstance.transform.up * 2500);
			i = 0;
		}
	        else if (i > RefireRate)
	            i = 0;
		i += Time.deltaTime;

		//if ((Time.time - missileStartTime) > 1)
		//{
		//    Destroy(bulletInstance);
		//}

		//timer += Time.deltaTime;
		//Debug.Log ("Diff: " + (timer - Time.time));
	}
}
