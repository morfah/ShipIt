using UnityEngine;
using System.Collections;

public class Experience : MonoBehaviour {
	private float experience = 0;
	private int level = 1;
	public float ExpBonus = 1.0f;

	public static float[] ExperienceTargets = new float[] {
		0, 100, 200, 400, 800, 1600, 3200, 6400, 12800, 25600, 51200,
		102400, 204800, 409600, 819200
	};
	public static int LEVELCAP = ExperienceTargets.Length;
	public static float EXPERIENCECAP = ExperienceTargets[LEVELCAP - 1];

	// Use this for initialization
	void Start () {
		// Get Experience and Level from Playerprefs.
		float LoadExperience = PlayerPrefs.GetFloat ("Experience");
		int LoadLevel = PlayerPrefs.GetInt ("Level");

		if (LoadExperience > 0.0f)
			experience = LoadExperience;
		if (LoadLevel > 0)
			level = LoadLevel;

		// Adjust Level (aka Level up or down because experience target changes between versions)
		AdjustLevel(CalculateLevel()); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GainExp (float exp) {
		// You gain experience
		experience +=  Mathf.Ceil(exp * ExpBonus);

		if (experience >= EXPERIENCECAP)
			experience = EXPERIENCECAP;

		PlayerPrefs.SetFloat ("Experience", experience); // Save new Experience value

		AdjustLevel(CalculateLevel()); // Adjust Level (aka Level up)
	}

	void AdjustLevel (int lvl) {
		// Adjust Level
		level = lvl;

		if (level >= LEVELCAP)
			level = LEVELCAP;

		PlayerPrefs.SetInt ("Level", level); // Save new Level value
//		Debug.Log ("You gained level " + level + "!");
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
			float ExpTarget = ExperienceTargets [level]; // level+1 is wrong here
			float ExpThisLevel = ExperienceTargets [level-1];
			float Percent = ((experience - ExpThisLevel) / (ExpTarget - ExpThisLevel));
			//Debug.Log (Percent);
			return Percent;
		}
	}

	public float GetExperience() {
		return experience;
	}

	public int GetLevel() {
		return level;
	}
}
