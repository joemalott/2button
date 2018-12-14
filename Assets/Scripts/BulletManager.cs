using System.Collections;
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
        // Bit shifting our layers to mask out in the raycast.
        int layerMask = (1 << 9);

        
        RaycastHit hit;

        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f, layerMask))
        {
            
        

         	if (hit.transform.tag == "Asteroid")
                hit.transform.GetComponent<AsteroidHealth>().Hit(hit.point);
            if (hit.transform.tag == "Drone")
                hit.transform.GetComponent<DroneHealth>().Hit(hit.point);
            if (hit.transform.tag == "Player")
            {
                var pm = hit.transform.GetComponent<PlayerManager>();
                pm.health -= 10;
                pm.Shield(hit.point);
            }
                 
// lalalal

            KillObject();

        } 
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f, ~layerMask))
        {

            KillObject();

        }
    
    }

    

}
