using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	public Texture2D ProgressbarTexture;

	private ApplyDamage hp;
	private Experience xp;
	private Player player;

	private int level;
	private int levelcap;
	private float experience;
	private float experiencecap;
	private float experiencerequired;
	private float percenttonextlevel;
	
	// Use this for initialization
	void Start () {
		hp = gameObject.GetComponent<ApplyDamage>();
		xp = gameObject.GetComponent<Experience>();
		player = gameObject.GetComponent<Player>();
		experiencecap = Experience.EXPERIENCECAP;
		levelcap = Experience.LEVELCAP;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI () {
		if (hp.HealthPoints <= 0) {
			Screen.lockCursor = false;
			player.dead = true;
			GUIStyle deathstyle = new GUIStyle ("box");
			deathstyle.fontSize = 25;

			// Death text
			GUI.Box (new Rect (Screen.width / 2 - 100, 50, 200, 50), "You Died", deathstyle);

			// Black & White
			// Blur
			// Slow motion

			// Buttons
			// Restart level
			if (GUI.Button (new Rect (Screen.width / 2 - 75, 100, 150, 75), "Restart level")) {
				Application.LoadLevel(Application.loadedLevel);
			}
			// Go to main menu
			else if(GUI.Button (new Rect (Screen.width / 2 - 75, 200, 150, 75), "Go to main menu")){
				Application.LoadLevel("MainMenu");
			}
			// Quit game
			else if(GUI.Button (new Rect (Screen.width / 2 - 75, 300, 150, 75), "Quit game")){
				Application.Quit();
			}

		} 
		else {

			level = xp.GetLevel ();
			experience = xp.GetExperience ();
			percenttonextlevel = xp.PercentToNextLevel ();
			if (level >= levelcap)
					experiencerequired = experiencecap;
			else
					experiencerequired = Experience.ExperienceTargets [level];

			// Health and Armor
			GUIStyle healthstyle = new GUIStyle ("box");
			healthstyle.fontSize = 25;

			GUI.Box (new Rect (0, Screen.height - 50, 160, 35), 
					"Hull: " + hp.HealthPoints + "%", healthstyle);
			GUI.Box (new Rect (Screen.width - 160, Screen.height - 50, 160, 35), 
					"Shield: " + hp.ArmorPoints + "%", healthstyle);

			// Level progressbar
			Color progressbarcolor = new Color ();
			progressbarcolor.r = percenttonextlevel * -1 + 1;
			progressbarcolor.g = percenttonextlevel;
			progressbarcolor.b = 0.0f;
			progressbarcolor.a = 0.6f;
			GUI.color = progressbarcolor;
			GUI.DrawTexture (
				new Rect (0, Screen.height - 15, Screen.width * percenttonextlevel, 15), 
				ProgressbarTexture
			);

			progressbarcolor.r = 0.5f;
			progressbarcolor.g = 0.5f;
			progressbarcolor.b = 0.5f;
			progressbarcolor.a = 0.5f;
			GUI.color = progressbarcolor;
			GUI.DrawTexture (
				new Rect (Screen.width * percenttonextlevel, Screen.height - 15, Screen.width, 15), 
				new Texture2D (1, 32)
			);
			GUI.color = Color.white;

			// Experience & Level information
			GUI.Box (new Rect (0, 0, 160, 35), "Level: " + level, healthstyle);

			GUIStyle experiencestyle = new GUIStyle ("label");
			experiencestyle.fontSize = 11;
			GUI.Label (
					new Rect (Screen.width / 2 - 150, Screen.height - 16, 300, 20), 
					(percenttonextlevel * (float)100.0f) + "% to next level. " +
					"(" + experience + " / " + experiencerequired + ")",
					experiencestyle
			);

			// Radar
//			GUI.Box (new Rect (Screen.width - (Screen.width / 6),0,Screen.width / 6, Screen.width / 6),
//			         "Radar");
//			Camera[] cameras = FindObjectsOfType(typeof(Camera)) as Camera[];
//			foreach (Camera cam in cameras) {
//				if (cam.name == "RadarCam") {
//					cam.rect = new Rect (0f,0f,0.5f,0.5f);
//				}
//			}


		}
	}

}
