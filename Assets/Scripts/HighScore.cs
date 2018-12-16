using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour {

	public int level;

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.HasKey("Level_" + level.ToString() + "_HighScore"))
        		GetComponent<TextMesh>().text = "High Score: " + PlayerPrefs.GetInt("Level_" + level.ToString() + "_HighScore").ToString();

	}
	
}
