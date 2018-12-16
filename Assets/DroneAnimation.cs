using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAnimation : MonoBehaviour {

	public Animator anim;

	// Use this for initialization
	void OnEnable () {
		anim.Play("warp");
	}
	
}
