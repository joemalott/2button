using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour {


	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("left") && Input.GetButton("right") )
			SceneManager.LoadScene("Hub");


	}
}
