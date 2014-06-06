using UnityEngine;
using System.Collections;

public class Experience : MonoBehaviour {
	private int experience = 0;
	private int level = 1;
	public float ExpBonus = 1.0f;

	private static int[] ExperienceTargets = new int[] {
		0, 100, 200, 400, 800, 1600, 3200, 6400, 12800, 25600, 51200,
		102400, 204800, 409600
	};
	private static int LEVELCAP = ExperienceTargets.Length;
	private static int EXPERIENCECAP = ExperienceTargets[LEVELCAP - 1];

	// Use this for initialization
	void Start () {
		// Get Experience and Level from Playerprefs.
		int LoadExperience = PlayerPrefs.GetInt ("Experience");
		int LoadLevel = PlayerPrefs.GetInt ("Level");

		if (LoadExperience > 0)
			experience = LoadExperience;
		if (LoadLevel > 0)
			level = LoadLevel;


		// Adjust Level (aka Level up or down because experience target changes between versions)
		AdjustLevel(CalculateLevel()); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GainExp (int exp) {
		// You gain experience
		experience += (int) Mathf.Ceil(exp * ExpBonus);

		if (experience >= EXPERIENCECAP)
			experience = EXPERIENCECAP;

		PlayerPrefs.SetInt ("Experience", experience); // Save new Experience value

		AdjustLevel(CalculateLevel()); // Adjust Level (aka Level up)
	}

	void AdjustLevel (int lvl) {
		// Adjust Level
		level = lvl;

		if (level >= LEVELCAP)
			level = LEVELCAP;

		PlayerPrefs.SetInt ("Level", level); // Save new Level value
		Debug.Log ("You gained level " + level + "!");
	}

	int CalculateLevel () {
		for (int i = LEVELCAP; i >= 1; i--) {
			if (experience >= ExperienceTargets[i-1])
				return i;
		}

		// Fix compile error
		return level;
	}

	// Returns a percentage to next level
	public float PercentToNextLevel() {
		if (level + 1 > LEVELCAP)
			return 1.0f;
		else {
			int ExpTarget = ExperienceTargets [level]; // level+1 is wrong here
			int ExpThisLevel = ExperienceTargets [level-1];
			float Percent = (((float)experience - (float)ExpThisLevel) / ((float)ExpTarget - (float)ExpThisLevel));
			//Debug.Log (Percent);
			return Percent;
		}
	}

	public int GetExperience() {
		return experience;
	}

	public int GetLevel() {
		return level;
	}
}
