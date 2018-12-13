using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour {

	public GameObject asteroid;

    public float spawnRadius = 100f;
    public float despawnDistance = 150f;

    public float spawnRate = 0.1f;

    private int pooledAmount = 30;
    List<GameObject> debris;

 
    private void Start()
    {
       
        debris = new List<GameObject>();

        StartCoroutine(StartSpawning());


        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(asteroid);
            obj.SetActive(false);
            debris.Add(obj);
        }

    }

    IEnumerator StartSpawning ()
    {
    	Spawn();
    	return WaitTimer();
    }

    IEnumerator WaitTimer ()
    {
    	yield return new WaitForSeconds(spawnRate);
    	StartCoroutine(StartSpawning());
    }



    public void Spawn()
    {

        if (gameObject.activeInHierarchy)
        {
            for (int i = 0; i < debris.Count; i++)
            {
                if (debris[i].activeInHierarchy)
                {
                    
                    Vector3 direction = debris[i].transform.position - transform.position;

                    if (direction.magnitude > despawnDistance)
                        debris[i].SetActive(false);
                    
                }
            }
        }

        if (gameObject.activeInHierarchy)
        {
            for (int i = 0; i < debris.Count; i++)
            {
                if (!debris[i].activeInHierarchy)
                {
                	
                    debris[i].transform.position = Utilities.RandomPointOnUnitCircle(spawnRadius) + transform.position;
                    debris[i].transform.rotation = transform.rotation;
                    debris[i].SetActive(true);
                    break;
                }
            }
        }
    }


  

    void OnDisable ()
    {

        foreach (GameObject asteroid in debris)
        {
            Destroy(asteroid);
        }

    }
}
