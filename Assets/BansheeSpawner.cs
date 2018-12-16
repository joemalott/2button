using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BansheeSpawner : MonoBehaviour {

	 public GameObject enemy;

    public float spawnRadius;

    public float despawnDistance;



    List<GameObject> enemies;

 
    private void Start()
    {
       
        enemies = new List<GameObject>();
  
        StartCoroutine(Spawn());

    }

    public IEnumerator Spawn()
    {

            yield return new WaitForSeconds(14f);

    	
    			GameObject banshee = Instantiate(enemy, Utilities.RandomPointOnUnitCircle(spawnRadius) + transform.position, Quaternion.LookRotation(transform.position, Vector3.up));
	
                Debug.Log("Spawned Banshee");

				enemies.Add(banshee);  

			

    		

    		foreach (GameObject enemy in enemies)
    		{
    			float dist = Vector3.Distance(enemy.transform.position, transform.position);
            	if (dist >= despawnDistance)
            	{

                    enemy.transform.position = Utilities.RandomPointOnUnitCircle(spawnRadius) + transform.position;
            		enemy.transform.rotation = Quaternion.LookRotation(transform.position, Vector3.up);
                    Debug.Log("Moved Banshee");
            	}
    		}

          

            BounceBack();

    }


    IEnumerator BounceBack ()
    {
        
        yield return new WaitForSeconds(1f);
        Spawn();
    }

    void OnDisable()
    {
        enemies.Remove(gameObject);
    }
  

}
