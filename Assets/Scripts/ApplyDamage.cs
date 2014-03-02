using UnityEngine;
using System.Collections;

public class ApplyDamage : MonoBehaviour {
	public float MissileDamage;

	// Use this for initialization
	void Start () {
		//Default variables
		if (MissileDamage == 0f)
			MissileDamage = 25f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision collision) {
		foreach (ContactPoint contactpoint in collision.contacts){
			switch (contactpoint.otherCollider.tag) {
			case "Missile":
				//gameObject.
				break;
			default:
				break;
			}
		}
	}
}
