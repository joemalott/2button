using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Director : MonoBehaviour {


        public static Director instance = null;    //Static instance of Director which allows it to be accessed by any other script.

        public int score;

        public TextMeshProUGUI scoreText;
        


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
        
            InvokeRepeating("CheckStuff", 1f, 1f);

        }


        void CheckStuff () {
            scoreText.text = score.ToString();
        }



}