using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	 public GameObject enemy;

    public float spawnRadius = 100f;

    public float despawnDistance = 150f;

    public float spawnRate = 2f;

    private int pooledAmount = 10;
    List<GameObject> enemies;

 
    private void Start()
    {
       
        enemies = new List<GameObject>();

        StartCoroutine(StartSpawning());


        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(enemy);
            obj.SetActive(false);
            enemies.Add(obj);
        }

    }

    IEnumerator StartSpawning ()
    {
    	Spawn();
    	return WaitTimer();
    }

    IEnumerator WaitTimer ()
    {

        float tempSpawnRate = spawnRate - (0.2f * PlayerManager.instance.powerLevel);

    	yield return new WaitForSeconds(tempSpawnRate);
    	StartCoroutine(StartSpawning());
    }



    public void Spawn()
    {

         if (gameObject.activeInHierarchy)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].activeInHierarchy)
                {
                    
                    Vector3 direction = enemies[i].transform.position - transform.position;

                    if (direction.magnitude > despawnDistance)
                        enemies[i].SetActive(false);
                    
                }
            }
        }

        if (gameObject.activeInHierarchy)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].activeInHierarchy)
                {
                    enemies[i].transform.position = Utilities.RandomPointOnUnitCircle(spawnRadius) + transform.position;
                    enemies[i].transform.rotation = transform.rotation;
                    enemies[i].SetActive(true);
                    break;
                }
            }
        }
    }


  

    void OnDisable ()
    {

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

    }

}
