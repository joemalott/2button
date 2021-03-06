﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public float speed = 15f;
    private float rotateSpeed;

    private CharacterController controller;

    private Vector3 lastRotation;

    public int powerLevel;
    private int savedPowerLevel;

    public int health = 100;

    public float fireTime = 0.5f;

    public GameObject shieldParticles;

    public GameObject playerDeathParticles;

    public GameObject spaceShip;

    public Animator anim;

    bool canBeDamaged= true;

    bool dying = false;

    bool canMove = false;
 

    public CannonManager[] cannons;
  
  	public static PlayerManager instance = null;    //Static instance of Director which allows it to be accessed by any other script.
       

        //Awake is always called before any Start functions
        void Awake()
        {
            //Check if instance already exists
            if (instance == null)
                
                //if not, set instance to this
                instance = this;
            
            //If instance already exists and it's not this:
            else if (instance != this)
                
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a Director.
                Destroy(gameObject);    
            
        }

    
    void Start() {
       controller = GetComponent<CharacterController>();
       lastRotation = transform.eulerAngles;

       rotateSpeed = speed * speed;

       powerLevel = 1;
       savedPowerLevel = 1;

       InvokeRepeating("CheckStats", 0, 0.2f);

       StartCoroutine(StartWait());
    }

    void FixedUpdate()
    {

    	health = Mathf.Clamp(health, 0, 200);

        if (Input.GetButton("left") && !Input.GetButton("right") && canMove == true)
        {
            Vector3 stickRotation = new Vector3(lastRotation.x, lastRotation.y - (rotateSpeed * Time.deltaTime), lastRotation.z);
            lastRotation = stickRotation;
        }

        if (Input.GetButton("right") && !Input.GetButton("left") && canMove == true)
        {
            Vector3 stickRotation = new Vector3(lastRotation.x, lastRotation.y + (rotateSpeed * Time.deltaTime), lastRotation.z);
            lastRotation = stickRotation;
        }
        
       
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, lastRotation, Time.deltaTime * rotateSpeed);

        var tempSpeed = speed;

        if (Input.GetButton("right") && Input.GetButton("left") && canMove == true)
        {
        	tempSpeed = speed + 20;
        } else {
        	tempSpeed = speed;
        }


        controller.Move( transform.forward * tempSpeed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        if (Director.instance != null)
          Director.instance.playerHealth = health;

    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.transform.tag == "PowerUp")
        {
            powerLevel += 1;
            powerLevel = Mathf.Clamp(powerLevel, 1, 5);

            Destroy(collision.gameObject, 0.1f);
        }

        if (collision.gameObject.transform.tag == "Tether")
        {
          int toLevel = collision.GetComponent<Tether>().levelToLoad;
          anim.Play("accelerate");
          StartCoroutine(WaitToLoad(toLevel));
        }

        if (collision.gameObject.transform.tag == "Health")
        {
          health += 10;
          powerLevel = Mathf.Clamp(powerLevel, -100, 100);

          Destroy(collision.gameObject, 0.1f);
        }

    }


    void CheckStats () {

      if (health <= 0)
      {
        Death();
      }

    	if (savedPowerLevel != powerLevel)
    	{


    		   if (powerLevel >= 1)
               {
		    		cannons[0].gameObject.SetActive(true);
		            cannons[1].gameObject.SetActive(false);
		            cannons[2].gameObject.SetActive(false);
		            cannons[3].gameObject.SetActive(false);
		            cannons[4].gameObject.SetActive(false);

		            speed = 15f;

		            

		            fireTime = 0.5f;

		            savedPowerLevel = powerLevel;
               }

    		   if (powerLevel >= 2)
               {
    		
                cannons[3].gameObject.SetActive(true);
                cannons[4].gameObject.SetActive(true);

                fireTime = 0.45f;

                savedPowerLevel = powerLevel;
               }

    		   if (powerLevel >= 3)
               {
                
                
                fireTime = 0.35f;

                speed = 17;

                savedPowerLevel = powerLevel;
               }

    		   if (powerLevel >= 4)
               {
   
                cannons[1].gameObject.SetActive(true);
                cannons[2].gameObject.SetActive(true);

      

                savedPowerLevel = powerLevel;
               }

    		   if (powerLevel >= 5)
               {
    		   	    fireTime = 0.3f;

                speed = 20f;
 
                savedPowerLevel = powerLevel;
               }

        }

    }

    IEnumerator StartWait()
    {
    	yield return new WaitForSeconds(1.5f);

      canMove = true;

    	StartCoroutine(InitiateFire());
    }


   IEnumerator InitiateFire()
   {
            foreach (CannonManager cannon in cannons)
            {
                cannon.Fire();
            }

           
            return WaitTimer();

   }

   IEnumerator WaitTimer()
   {
    yield return new WaitForSeconds(fireTime);
    StartCoroutine(InitiateFire());
   }

   void OnControllerColliderHit(ControllerColliderHit collision)
    {
    
     	
	     	if (canBeDamaged)
	     	{
	     		health -= 10;
	     		Shield(collision.point);
	     		canBeDamaged = false;
	     		StartCoroutine(InvulnerableTimer());
	     	}
     	
     
    }


   public void Shield (Vector3 hitPoint)
   {
   		Vector3 relativePos = hitPoint - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos);
       
   		GameObject shieldparts = Instantiate(shieldParticles, transform.position, rotation);
   		shieldparts.transform.SetParent(gameObject.transform);
   }
    
    IEnumerator InvulnerableTimer ()
    {
    	yield return new WaitForSeconds(1.5f);
    	canBeDamaged = true;
    }

    public void Death ()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (PlayerPrefs.GetInt("Level_" + scene.buildIndex.ToString() + "_HighScore") < Director.instance.score)
        {
          PlayerPrefs.SetInt("Level_" + scene.buildIndex.ToString() + "_HighScore", Director.instance.score);

          PlayerPrefs.Save();
        }
       
        if (!dying)
          StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        dying = true;
        Instantiate(playerDeathParticles, transform.position, Quaternion.identity);
        spaceShip.SetActive(false);
        Time.timeScale = 0.2f;
        yield return StartCoroutine(WaitForRealSeconds(4f));
        Time.timeScale = 1f;
        SceneManager.LoadScene("Hub");
    }


    public static IEnumerator WaitForRealSeconds(float time)
     {
         float start = Time.realtimeSinceStartup;
         while (Time.realtimeSinceStartup < start + time)
         {
             yield return null;
         }
     }


     IEnumerator WaitToLoad(int toLevel)
     {

      yield return new WaitForSeconds(1.5f);
      SceneManager.LoadScene(toLevel);
     }


}
