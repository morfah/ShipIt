using UnityEngine;
using System.Collections;

public class Experience : MonoBehaviour {
	public long experience;
	public long level = 1;
	public float ExpBonus = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GainExp (long exp) {
		// You gain experience
		experience += (long) Mathf.Ceil(exp * ExpBonus);
		CheckIfLevelChanged ();
	}

	void GainLvl (long lvl) {
		// You level up
		level += lvl;
		Debug.Log ("Level up!");
	}

	void CheckIfLevelChanged () {
		long levelBefore = level;
		long levelsToGain = 0;

		levelsToGain = (long)Mathf.Floor(experience / (100 * (levelBefore * levelBefore)));
		
		if (levelsToGain != 0) {
			GainLvl(levelsToGain);
		}


	}
}
