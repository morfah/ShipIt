using UnityEngine;
using System.Collections;

public class Missle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // constantForce
        rigidbody.constantForce.force = transform.up * 500;
        //rigidbody.constantForce.relativeForce = transform.up * 1000;

        //Add force
        //rigidbody.AddForce(transform.up * 2500);
        
        // lek
        //rigidbody.constantForce.force = Vector3.up * 10000000;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
