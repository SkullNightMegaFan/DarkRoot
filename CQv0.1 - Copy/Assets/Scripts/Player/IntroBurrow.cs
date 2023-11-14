using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBurrow : MonoBehaviour
{
   private void OnTriggerStay2D(Collider2D other)
    {
             PlayerController playerController = other.GetComponent<PlayerController>();
        
        if (other.CompareTag("Player") && playerController.isInteracting)
        {
           Debug.Log("Player has touched the burrow");           
            
            other.attachedRigidbody.position = playerController.exitBurrowPoint;
            //other.attachedRigidbody.position = playerController.introBurrowPoint;
            

          
        }
        else if (other.CompareTag("PlayerProjectiles"))
        {
            other.attachedRigidbody.position = playerController.exitBurrowPoint;
            Debug.Log("Bullet has entered the Burrow");
            //other.attachedRigidbody.position = playerController.exitBurrowPoint;
                //other.attachedRigidbody.position = playerController.exitBurrowPoint;

        }
    }
}
