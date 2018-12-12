using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

	public float health = 60f;

	void Update () 
	{
		KillObject();
	}

	void KillObject() {
		if (health <= 0f)
		{
			Destroy(this.gameObject, 0f);
		}
	}

}
