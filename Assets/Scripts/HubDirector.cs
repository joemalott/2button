using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HubDirector : MonoBehaviour {


        public static HubDirector instance = null;    //Static instance of HubDirector which allows it to be accessed by any other script.

        public Camera cam;

        bool canZoom = false;

        //Awake is always called before any Start functions
        void Awake()
        {

            cam = Camera.main;

            //Check if instance already exists
            if (instance == null)
                
                //if not, set instance to this
                instance = this;
            
            //If instance already exists and it's not this:
            else if (instance != this)
                
                //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a HubDirector.
                Destroy(gameObject);    

            LoadGame();

            StartCoroutine(WaitABit(1f));


              
        }


        IEnumerator WaitABit(float time)
        {
            yield return new WaitForSeconds(time);

            canZoom = true;
        }



        void LoadGame()
        {

        	// First Check if there is even high score data to begin with. 

        	if (!PlayerPrefs.HasKey("Level_1_HighScore"))
        		PlayerPrefs.SetInt("Level_1_HighScore", 0);
            
       

        	PlayerPrefs.Save();

        }

        void Update ()
        {
            if (canZoom)
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 10f, 1f * Time.deltaTime);
        }



}