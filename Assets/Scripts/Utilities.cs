using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities {

	public static Vector3 RandomPointOnUnitCircle(float radius)
	{
	     float angle = Random.Range (0f, Mathf.PI * 2);
	     float x = Mathf.Sin (angle) * radius;
	     float z = Mathf.Cos (angle) * radius;

	     return new Vector3 (x, 0, z);

	}
}
