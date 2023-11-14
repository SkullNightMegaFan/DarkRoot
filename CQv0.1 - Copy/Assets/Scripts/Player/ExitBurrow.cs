using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBurrow : MonoBehaviour
{
    // Start is called before the first frame update
     private void OnTriggerStay2D(Collider2D other)
    {
             PlayerController playerController = other.GetComponent<PlayerController>();
        
        if (other.CompareTag("Player") && playerController.isInteracting)
        {
           Debug.Log("Player has touched the burrow");           
            
            other.attachedRigidbody.position = playerController.introBurrowPoint;
            //other.attachedRigidbody.position = playerController.introBurrowPoint;
            

          
        }
        else if (other.CompareTag("PlayerProjectiles"))
        {
            other.attachedRigidbody.position = playerController.introBurrowPoint;
            Debug.Log("Bullet has entered the Burrow");
            //other.attachedRigidbody.position = playerController.exitBurrowPoint;
                //other.attachedRigidbody.position = playerController.exitBurrowPoint;

        }
    }
}
