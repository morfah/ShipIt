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
		if (GUI.Button (new Rect (Screen.width / 2 - 75, 200, 150, 100), "New Game")) {
			print ("You clicked New Game!");
			Application.LoadLevel("Test01");
		}
		else if(GUI.Button (new Rect (Screen.width / 2 - 75, 350, 150, 100), "Load Game")){
			print ("You clicked Load Game!");
		}
	}
}
