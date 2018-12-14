using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneHealth : MonoBehaviour {

	public float health = 10f;

	public float chanceToSpawnPowerUp = 1f;

	public GameObject droneDeathParticles;

	public GameObject powerUp;

	public void Hit(Vector3 hitLocation)
	{
		health -= 10f;

		
		
		if (health <= 0f)
		{
			Instantiate(droneDeathParticles, gameObject.transform.position, Quaternion.identity);
			Director.instance.score += 10;
			if (Random.Range(0,20) <= chanceToSpawnPowerUp)
			{
				Instantiate(powerUp, gameObject.transform.position, Quaternion.identity);
			}

			gameObject.SetActive(false);
			
		}

	}
}
