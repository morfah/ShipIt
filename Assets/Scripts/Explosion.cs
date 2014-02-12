using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		light.range = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		light.range = light.range - 15f*Time.deltaTime;

		if (light.range < .1 && gameObject.name.EndsWith("(Clone)"))
			Destroy(gameObject);
	}
}
