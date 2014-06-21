using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int MovementSpeed = 10;
	public float TurnSpeed = 5f;
	public float pushPower = 150.0f;
	public int Level = 1;
	
	protected GameObject player;
	protected Vector3 raycastOrigin;
	protected Vector3 raycastDirection;
	protected RaycastHit raycastHit;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");
//		StartYPosition = transform.position.y;
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic || body.tag == "Missile" || body.tag == "Player" || body.tag == "Enemy")
			return;
		
		Vector3 pushDir = new Vector3(hit.moveDirection.x, hit.moveDirection.y, hit.moveDirection.z);
		body.velocity = pushDir * (pushPower / body.mass);
	}

	protected void LookAtPlayer(){
		Vector3 moveDirection = player.transform.position - transform.position;
		moveDirection.y = 0; 
		moveDirection.Normalize();

		float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
		transform.rotation = 
			Quaternion.Slerp(transform.rotation, 
			                 Quaternion.Euler(0, targetAngle, 0), 
			                 TurnSpeed * Time.deltaTime);
	}

	protected void MoveToPlayer(bool moveaway) {
		gameObject.renderer.material.color = Color.magenta;

		CharacterController controller = GetComponent<CharacterController>();
		Vector3 moveDirection;

		if (moveaway) moveDirection = transform.position - player.transform.position;
		else moveDirection = player.transform.position - transform.position;

		//moveDirection.y = player.GetComponent<Player>().StartYPosition - transform.position.y; 
		moveDirection.Normalize();

		moveDirection *= MovementSpeed;

//		Debug.Log ("movedir = " + moveDirection);
		
		controller.Move(moveDirection * Time.deltaTime);
//		float step = MovementSpeed * Time.deltaTime;
//		transform.position = Vector3.MoveTowards (transform.position, player.transform.position, step);

	}

	public int GetEnemyLevel () {
		return Level;
	}
}
