using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	public Texture2D ProgressbarTexture;

	private ApplyDamage ad;
	private Experience ex;

	private int level;
	private int levelcap;
	private float experience;
	private float experiencecap;
	private float xpfornextlevel;
	
	// Use this for initialization
	void Start () {
		ad = gameObject.GetComponent<ApplyDamage>();
		ex = gameObject.GetComponent<Experience>();
		experiencecap = Experience.EXPERIENCECAP;
		levelcap = Experience.LEVELCAP;
	}
	
	// Update is called once per frame
	void Update () {
		level = ex.GetLevel();
		experience = ex.GetExperience();
		if (level >= levelcap)
			xpfornextlevel = experiencecap;
		else
			xpfornextlevel = Experience.ExperienceTargets [level];
	}

	void OnGUI () {
		// Health and Armor
		GUIStyle healthstyle = new GUIStyle ("label");
		healthstyle.fontSize = 30;
		GUI.Box(new Rect(0, Screen.height - 80, 350, 50), 
		        "HULL: " + ad.HealthPoints + "  SHIELD: " + ad.ArmorPoints, healthstyle);

		// Level progressbar
		Color progressbarcolor = new Color ();
		progressbarcolor.r = ex.PercentToNextLevel() * -1 + 1;
		progressbarcolor.g = ex.PercentToNextLevel();
		progressbarcolor.b = 0.0f;
		progressbarcolor.a = 0.6f;
		GUI.color = progressbarcolor;
		GUI.DrawTexture (
			new Rect (0, Screen.height - 15, Screen.width * ex.PercentToNextLevel (), 15), 
			ProgressbarTexture
		);
		progressbarcolor.r = 0.5f;
		progressbarcolor.g = 0.5f;
		progressbarcolor.b = 0.5f;
		progressbarcolor.a = 0.5f;
		GUI.color = progressbarcolor;
		GUI.DrawTexture (
			new Rect (Screen.width * ex.PercentToNextLevel (), Screen.height - 15, Screen.width, 15), 
			new Texture2D(1,32)
		);
		GUI.color = Color.white;
		
		// Experience & Level information
		GUI.Box(new Rect(0, 0, 300, 50), 
		        "Level: " + level, healthstyle);
		GUIStyle experiencestyle = new GUIStyle ("label");
		experiencestyle.fontSize = 11;
		GUI.Label(
			new Rect(Screen.width / 2 - 150, Screen.height - 16, 300, 20), 
			(ex.PercentToNextLevel() * (float)100.0f) + "% to next level. " +
			"(" + experience + " / " + xpfornextlevel + ")",
			experiencestyle
		);

		// Death text
		if (ad.HealthPoints <= 0) {
		GUI.Box(new Rect(Screen.width / 2-50, 50, 100, 20), 
		        "You Died");
		}
	}

}
