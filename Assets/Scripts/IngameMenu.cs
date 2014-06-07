using UnityEngine;
using System.Collections;

public class IngameMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bool Escape = Input.GetKey(KeyCode.Escape);
		if (Escape) {
			Application.LoadLevel("MainMenu");
		}
	}
}
