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
		Debug.Log ("You gained level " + level + "!");
	}

	void CheckIfLevelChanged () {
		long levelBefore = level;
		long levelsToGain = 0;

		/*
		 * XP0    Lvl1
		 * XP150  Lvl2
		 * XP300  Lvl3
		 * XP450  Lvl4
		 * XP600  Lvl5
		 * 
		 * XP0        Lvl1
		 * XP100      Lvl2
		 * XP250      Lvl3
		 * XP375      Lvl4
		 * XP562.5    Lvl5
		 * XP843.75   Lvl6
		 * XP1265.625 Lvl7

		 */


//		for (int i = 3; i <= 10; i++) {
//			Debug.Log ("Level" + i + " = XP" + ((100 * i-1) * 1.5f));
//		}

//		for (int i = 0; i < 10000; i++){
//			Debug.Log (i + "XP = Lvl" + (long)Mathf.Floor(i / (100 * ( * 1.5f)));
//		}




		levelsToGain = (long)Mathf.Floor(experience / (100 * (levelBefore * levelBefore)));
		
		if (levelsToGain != 0) {
			GainLvl(levelsToGain);
		}


	}
}
