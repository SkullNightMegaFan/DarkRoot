using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinScript : MonoBehaviour
{

      private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //in order to make sure it properly works, in my experience at least 
            //ensure that the scene you want to load is in the build settings. 
           SceneManager.LoadScene("YouWin");
           Debug.Log("You Win!");
   
           }
          
        
        else
        {
            //////////
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

