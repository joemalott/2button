using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform player;

	public float cameraLerpSpeed = 3f;

	

	void LateUpdate () 
	{

		if (player == null)
		{
			player = GameObject.Find("Player").transform;
		}

		if (player != null)
		{
			transform.position = player.transform.position;
		}
	}
	

}
