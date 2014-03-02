using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Initial light range is 10
		light.range = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		// Reduce light range every frame. This value was decided by trial and error.
		light.range = light.range - 15f*Time.deltaTime;

		// When the light range is low enough remove it.
		// TODO: Is there a better way to spawn objects than to Instantiate() an existed GameObject?
		if (light.range < .1 && gameObject.name.EndsWith("(Clone)"))
			Destroy(gameObject);

		//TODO Sound
		//TODO https://docs.unity3d.com/Documentation/ScriptReference/Rigidbody.AddExplosionForce.html
	}
}
