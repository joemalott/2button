using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererManager : MonoBehaviour {

	TrailRenderer ts;

	private void OnEnable() {
  		ts = gameObject.GetComponent<TrailRenderer>();
  	}

    private void OnDisable()
    {
    	ts.Clear();
    }
}
