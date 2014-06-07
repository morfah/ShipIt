using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if (GUI.Button (new Rect (Screen.width / 2 - 75, 100, 150, 75), "Goto Test01")) {
			Application.LoadLevel("Test01");
		}
		else if(GUI.Button (new Rect (Screen.width / 2 - 75, 200, 150, 75), "Goto kuroto_test")){
			Application.LoadLevel("kuroto_test");
		}
		else if(GUI.Button (new Rect (Screen.width - 75, 0, 75, 50), "Quit")){
			print ("You clicked Quit Game!");
			Application.Quit();
		}
	}
}
