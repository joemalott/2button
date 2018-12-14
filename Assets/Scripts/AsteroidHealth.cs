using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : MonoBehaviour {

	public float health = 20f;

	public float chanceToSpawnPowerUp = 1f;

	public GameObject asteroidHitParticles;
	public GameObject asteroidBreakParticles;

	public GameObject powerUp;

	public void Hit(Vector3 hitLocation)
	{
		health -= 10f;

		if (health > 0f)
		{
		
			Instantiate(asteroidHitParticles, hitLocation, Quaternion.identity);
			
		}
		
		
		if (health <= 0f)
		{
			Instantiate(asteroidBreakParticles, gameObject.transform.position, Quaternion.identity);

			Director.instance.score += 10;

			if (Random.Range(0,10) <= chanceToSpawnPowerUp)
			{
				Instantiate(powerUp, gameObject.transform.position, Quaternion.identity);
			}

			gameObject.SetActive(false);
			
		}

	}
}
