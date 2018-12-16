using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHealth : MonoBehaviour {

	public float health = 10f;

	public float chanceToSpawnPowerUp = 5f;

	public GameObject droneDeathParticles;

	public GameObject[] pickups;

	public void OnEnable()
	{
		health = Director.instance.score / 2000 * 10;
		health = Mathf.Clamp(health, 10, 20);
	}

	public void Hit(Vector3 hitLocation)
	{
		health -= 10f;

		
		
		if (health <= 0f)
		{
			Instantiate(droneDeathParticles, gameObject.transform.position, Quaternion.identity);
			Director.instance.score += 10;
			if (Random.Range(0,100) <= chanceToSpawnPowerUp)
			{
				int index = Random.Range(0, pickups.Length);
				Instantiate(pickups[index], gameObject.transform.position, Quaternion.identity);
			}

			gameObject.SetActive(false);
			
		}

	}
}
