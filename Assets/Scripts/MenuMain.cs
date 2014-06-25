using UnityEngine;
using System.Collections;

public class MenuMain : MonoBehaviour {
	string[] buttonTexts = new string[]
	{
		"Continue",
		"New Game",
		"Load Game",
		"Options",
		"Quit Game"
	};
	
	void OnGUI() {
		float buttonWidth = Screen.width / 5;
		float buttonHeight = Screen.height / 10;
		float buttonCenter = (Screen.width / 2) - (buttonWidth / 2);
		float buttonSpacing = buttonHeight / 5;
		float buttonDisplace = Screen.height / buttonTexts.Length;

		// Buttons
		for (int i = 0; i < buttonTexts.Length; i++) {
			float buttonVerticalPos = buttonDisplace + (i*buttonHeight) + (buttonSpacing * i);
			if (GUI.Button (new Rect (buttonCenter, buttonVerticalPos, buttonWidth, buttonHeight), buttonTexts[i]))
			{
				switch (i) {
				case 0:
					Debug.Log ("Clicked Continue");
					break;
				case 1:
					Debug.Log ("Clicked New Game");
					Application.LoadLevel("map01");
					break;
				case 2:
					Debug.Log ("Clicked Load Game");
					break;
				case 3:
					Debug.Log ("Clicked Options");
					break;
				case 4:
					Debug.Log ("Clicked Quit Game");
					Application.Quit();
					break;
				}
			}
		}
	}
}
