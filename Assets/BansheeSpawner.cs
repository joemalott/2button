using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BansheeSpawner : MonoBehaviour {

	 public GameObject enemy;

    public float spawnRadius = 100f;

    public float despawnDistance = 150f;

    int tempScore;

    List<GameObject> enemies;

 
    private void Start()
    {
       
        enemies = new List<GameObject>();

        tempScore = Director.instance.score;
    	
       
        StartCoroutine(Spawn());
      	



    }

   




    public IEnumerator Spawn()
    {


    	if (Director.instance.score - tempScore >= 100)
    		{
    			GameObject banshee = Instantiate(enemy, Utilities.RandomPointOnUnitCircle(spawnRadius) + transform.position, transform.rotation);
	
				enemies.Add(banshee);  

				tempScore = Director.instance.score;

    		}

    		yield return new WaitForSeconds(1f);

    		foreach (GameObject enemy in enemies)
    		{
    			float dist = Vector3.Distance(enemy.transform.position, transform.position);
            	if (dist >= despawnDistance)
            	{
            		enemies.Remove(enemy);
            		Destroy(enemy.gameObject);
            	}
    		}

    }


  

}
