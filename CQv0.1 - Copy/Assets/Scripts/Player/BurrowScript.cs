using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurrowScript : MonoBehaviour
{
    
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
           
            if (playerController.isInteracting);
            {
                Debug.Log("Player is interacting");
                playerController.rb.position = playerController.introBurrowPoint;
            }
          
        }
        else
        {
            //////////
        }
    }
}
