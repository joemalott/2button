﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAI : MonoBehaviour {

	public Transform target;
	 
	public float moveSpeed = 6.0f;
	public float rotationSpeed = 3.0f;
	 
	private int minDistance = 10;
	private int originalSafeDistance = 30;
	private int safeDistance = 30; 
	 
	public enum AIState {Idle, Seek, Flee }

	public AIState currentState;

	bool checkPlayer = true;


	// Use this for initialization
	void OnEnable () {

		checkPlayer = true;
		
		target = GameObject.FindWithTag("Player").transform;

		if (target != null)
		{
			checkPlayer = true;
			currentState = AIState.Seek;
		}

		else if (target == null)
		{
			checkPlayer = false;
			currentState = AIState.Idle;
			gameObject.SetActive(false);
		}

	}


	


	void  Update (){
	    if (checkPlayer == true)
	    {
	    	switch(currentState){
	        case AIState.Idle:
	            break;
	        case AIState.Seek:
	            Seek();
	            break;
	        case AIState.Flee:
	            Flee();
	            break;
	    	}
	    }
	}
	 
	void Seek (){
	    Vector3 direction = target.position - transform.position;
	   direction.y = 0;
	 
	   transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
	 
	   if(direction.magnitude > minDistance){
	 
	      Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
	 
	      transform.position += moveVector;
	 
	   } else {
	   	currentState = AIState.Flee;
	   }
	}
	 
	void Flee (){
	    Vector3 direction = transform.position - target.position;
	   direction.y = 0;
	   
	   if(direction.magnitude < safeDistance){
	      transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
	      Vector3 moveVector = direction.normalized * moveSpeed * Time.deltaTime;
	      transform.position += moveVector;
	   }  else {
	   	safeDistance = Random.Range(minDistance, originalSafeDistance);
	   	currentState = AIState.Seek;
	   }
	}
	
	
	 
}

