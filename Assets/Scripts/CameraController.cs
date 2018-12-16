using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform player;


	

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
