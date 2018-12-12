﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    public float bulletSpeed = 10f;

    public float deathTime = 3f;

    private TrailRenderer ts;

  	private void Awake() {
  		ts = gameObject.GetComponent<TrailRenderer>();
  	}

    private void OnEnable()
    {
        Invoke("KillObject", deathTime);
       
    }

    void KillObject()
    {
    	ts.Clear();
        gameObject.SetActive(false);

    }

    private void OnDisable()
    {
        CancelInvoke();
    }



    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * bulletSpeed;

        CheckCollision();
    }

    void CheckCollision()
    {
        int layerMask = 1 << 11;
        
        // Play a noise if an object is within the sphere's radius.
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, layerMask))
        {
            
            Debug.Log("hit");
            //  Change this float to the current damage indicator. 
            hit.transform.gameObject.GetComponent<EnemyManager>().health -= 10f;
            KillObject();

        } 
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10f, ~layerMask))
        {

            KillObject();

        }
    
    }

    

}