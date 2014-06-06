using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	private ApplyDamage ad;
	private Experience ex;
	
	// Use this for initialization
	void Start () {
		ad = gameObject.GetComponent<ApplyDamage>();
		ex = gameObject.GetComponent<Experience>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		// Level progressbar
//		float die = Random.Range (-Screen.width*2, Screen.width*2);
//		Debug.Log (die);
		//1731.343
//		Debug.Log ("PX: " + Screen.width * ex.PercentToNextLevel ());
//		Debug.Log ("ROUND PX: " + Mathf.Round (Screen.width * ex.PercentToNextLevel ()));
		GUI.color = Color.gray;
		GUI.DrawTexture (
			new Rect (0, Screen.height - 30, Screen.width * ex.PercentToNextLevel (), 30), 
			new Texture2D(32,32)
		);
		GUI.color = Color.white;

		// Health and Armor
		GUI.Box(new Rect(Screen.width / 5, Screen.height - 60, 150, 25), 
		        "HP: " + ad.HealthPoints + "  Armor: " + ad.ArmorPoints);
		
		// Experience & Level information
		GUI.Label(
			new Rect(Screen.width / 2 - 150, Screen.height - 25, 300, 25), 
			ex.GetExperience() + " XP  Level " + ex.GetLevel() +
			" (" + (ex.PercentToNextLevel() * (float)100.0f) + "% to next level.)"
		);

		// Death text
		if (ad.HealthPoints <= 0) {
		GUI.Box(new Rect(Screen.width / 2-50, 50, 100, 20), 
		        "You Died");
		}
	}

}
