using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float speed = 10.0f;
    private float rotateSpeed;

    private CharacterController controller;

    private Vector3 lastRotation;

    public int powerLevel;
    private int savedPowerLevel;

    public int health = 100;

    public float fireTime = 0.5f;
 

    public GameObject[] turrets;

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
            
            //Sets this to not be destroyed when reloading scene
            DontDestroyOnLoad(gameObject);

         

        }

    
    void Start() {
       controller = GetComponent<CharacterController>();
       lastRotation = transform.eulerAngles;

       rotateSpeed = speed * 10;

       powerLevel = 1;
       savedPowerLevel = 1;

       InvokeRepeating("CheckStats", 0, 1);

       StartCoroutine(InitiateFire());
    }

    void FixedUpdate()
    {

        if (Input.GetButton("left") && !Input.GetButton("right"))
        {
            Vector3 stickRotation = new Vector3(lastRotation.x, lastRotation.y - (rotateSpeed * Time.deltaTime), lastRotation.z);
            lastRotation = stickRotation;
        }

        if (Input.GetButton("right") && !Input.GetButton("left"))
        {
            Vector3 stickRotation = new Vector3(lastRotation.x, lastRotation.y + (rotateSpeed * Time.deltaTime), lastRotation.z);
            lastRotation = stickRotation;
        }
        
       
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, lastRotation, Time.deltaTime * rotateSpeed);

        controller.Move( transform.forward * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);


    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.transform.tag == "PowerUp")
        {
            powerLevel += 1;
            powerLevel = Mathf.Clamp(powerLevel, 1, 8);

            Destroy(collision.gameObject, 0.1f);
        }
    }


    void CheckStats () {

    	if (savedPowerLevel != powerLevel)
    	{


    		   if (powerLevel >= 1)
               {
    		   	turrets[0].SetActive(true);
            turrets[1].SetActive(false);
            turrets[2].SetActive(false);
            turrets[3].SetActive(false);
            turrets[4].SetActive(false);

            speed = 10f;

            rotateSpeed = speed * 10;

            fireTime = 0.5f;

                savedPowerLevel = powerLevel;
               }

    		   if (powerLevel >= 2)
               {
    		
                turrets[3].SetActive(true);
                turrets[4].SetActive(true);

                fireTime = 0.45f;

                savedPowerLevel = powerLevel;
               }

    		   if (powerLevel >= 3)
               {
                
                turrets[1].SetActive(true);
                turrets[2].SetActive(true);

                fireTime = 0.4f;

                speed = 15;

                rotateSpeed = speed * 9;

                savedPowerLevel = powerLevel;
               }

    		   if (powerLevel >= 4)
               {
   
                fireTime = 0.4f;

                savedPowerLevel = powerLevel;
               }

    		   if (powerLevel >= 5)
               {
    		   	    fireTime = 0.35f;

                speed = 20;

                rotateSpeed = speed * 8;

                savedPowerLevel = powerLevel;
               }

            if (powerLevel >= 6)
               {
                fireTime = 0.3f;

                savedPowerLevel = powerLevel;
               }

            if (powerLevel >= 7)
               {
                fireTime = 0.25f;

                speed = 25;

                rotateSpeed = speed * 7;

                savedPowerLevel = powerLevel;
               }

            if (powerLevel >= 8)
               {
                fireTime = 0.2f;

                speed = 30;

                rotateSpeed = speed * 6;

                savedPowerLevel = powerLevel;
               }

    	}

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
    

}
