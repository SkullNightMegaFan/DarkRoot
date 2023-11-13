using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurrowScript : MonoBehaviour
{
    
   private void OnTriggerStay2D(Collider2D other)
    {
                    PlayerController playerController = other.GetComponent<PlayerController>();

    
        if (other.CompareTag("Player") && playerController.isInteracting)
        {
           Debug.Log("Player has touched the burrow");
           //this line of logic doesn't work well, regardless of if the player 
           //is interacting or not. It will always trigger.
            // if (playerController.isInteracting = true);
            // {
            //     Debug.Log("Player is interacting with burrow");
            //     playerController.rb.position = playerController.introBurrowPoint;
            // }
            //playerController.rb.position = playerController.introBurrowPoint;
           
                    
            other.attachedRigidbody.position = playerController.exitBurrowPoint;
            //other.attachedRigidbody.position = playerController.introBurrowPoint;
            

          
        }
        else if (other.CompareTag("PlayerProjectiles"))
        {
            Debug.Log("Bullet has entered the Burrow");
            //the logic works, but the bullet dies when colliding with other trigger boxes, need to make an exception for burrows. 
            other.attachedRigidbody.position = playerController.introBurrowPoint;
        }
    }
}
