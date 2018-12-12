using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CannonManager : MonoBehaviour {

    public GameObject bullet;

    private GameObject turret;

    private int pooledAmount = 35;
    List<GameObject> bullets;

 
    private void OnEnable()
    {
        turret = this.gameObject;

        bullets = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(bullet);
            obj.SetActive(false);
            bullets.Add(obj);
        }

    }



    public void Fire()
    {

        if (gameObject.activeInHierarchy)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    bullets[i].transform.position = turret.transform.position;
                    bullets[i].transform.rotation = transform.rotation;
                    bullets[i].SetActive(true);
                    break;
                }
            }
        }
    }


  

    void OnDisable ()
    {

        foreach (GameObject bullet in bullets)
        {
            Destroy(bullet);
        }

    }


}
