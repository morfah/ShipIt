using UnityEngine;
using System.Collections;

public class VisualExperiencePoint : MonoBehaviour {
	float state1time = 1f; // Move towards Player
	float state2time = 1.5f; // Destroy self
	float spawnedTime;
	int state = 0;
	float randomposX, randomposY, randomposZ;

	// Use this for initialization
	void Start () {
		spawnedTime = Time.time;
		transform.renderer.material.color = Color.yellow;
		//transform.renderer.material.shader = Shader.

		randomposX = Random.Range (transform.position.x - 20f, transform.position.x + 20f);
		randomposY = Random.Range (transform.position.y - 20f, transform.position.y + 20f);
		randomposZ = Random.Range (transform.position.z - 20f, transform.position.z + 20f);
	}
	
	// Update is called once per frame
	void Update () {
		if (state == 0) { // Move to random position
			transform.position = Vector3.Lerp (transform.position, 
                                   new Vector3 (randomposX, randomposY, randomposZ), 
                                   Time.deltaTime * Random.Range(1f, 10f));
			transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform.position); // face the camera
		} else if (state == 1) { // Move towards player
			GameObject player = GameObject.FindGameObjectWithTag ("Player");
			transform.position = Vector3.Lerp (transform.position, 
                                   player.transform.position, 
                                   Time.deltaTime * 10f);
			transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").transform.position); // face the camera
			//transform.renderer.material.color.a = Mathf.Lerp(1.0f,0.0f,Time.deltaTime * 3f);
		} else if (state == 2) { // Destroy self
			Destroy (gameObject);
		}


		if ((Time.time - spawnedTime) > state2time){
			state = 2;
		}
		else if ((Time.time - spawnedTime) > state1time){
			state = 1;
		}
		else {
			state = 0;
		}

	}
}
