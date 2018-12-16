using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : MonoBehaviour {

	public float health = 20f;

	public float chanceToSpawnPowerUp = 5f;

	public GameObject asteroidHitParticles;
	public GameObject asteroidBreakParticles;

	public GameObject[] pickups;

	public void OnEnable()
	{
		health = Director.instance.score / 1000 * 10 + 10;
		health = Mathf.Clamp(health, 20, 80);
	}

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

			if (Random.Range(0,100) <= chanceToSpawnPowerUp)
			{
				int index = Random.Range(0, pickups.Length);
				Instantiate(pickups[index], gameObject.transform.position, Quaternion.identity);
			}

			gameObject.SetActive(false);
			
		}

	}
}
