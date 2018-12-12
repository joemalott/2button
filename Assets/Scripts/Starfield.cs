using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starfield : MonoBehaviour {

    public float rate = 30f;
    public float deadZone = 0.3f;
    public float originalRate = 15f;

    ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var emission = ps.emission;

        if (Mathf.Abs(Input.GetAxis("Horizontal")) >= deadZone || Mathf.Abs(Input.GetAxis("Vertical")) >= deadZone)
        {
            emission.rateOverTime = rate;
        } else {
            emission.rateOverTime = originalRate;
        }

    }
}
