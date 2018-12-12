using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform player;

	public float cameraLerpSpeed = 3f;

	

	void FixedUpdate () 
	{

		if (player == null)
		{
			player = GameObject.Find("Player").transform;
		}

		if (player != null)
		{
			transform.position = Vector3.Lerp(transform.position, player.transform.position, cameraLerpSpeed * Time.deltaTime);
		}
	}
	

}
