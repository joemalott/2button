using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

	public Vector3 rotateSpeed;

	public bool isAsteroid;

	void OnEnable()
	{
		if (isAsteroid)
			rotateSpeed = new Vector3(Random.Range(1,11), Random.Range(1,11), Random.Range(1,11));
	}
	
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(rotateSpeed * Time.deltaTime);
	}
}
