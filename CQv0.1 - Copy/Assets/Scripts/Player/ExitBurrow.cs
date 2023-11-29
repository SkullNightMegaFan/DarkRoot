using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBurrow : MonoBehaviour
{

    public Vector2 otherPosition;
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
    public void OnTriggerEnter2D(Collider2D other)
{
    
                 PlayerController playerController = other.GetComponent<PlayerController>();
                 BulletBehaviour bulletBehaviour = other.GetComponent<BulletBehaviour>();

        if (other.CompareTag("PlayerProjectiles"))
        {
            Debug.Log("Bullet has entered the burrow");
            otherPosition = playerController.introBurrowPoint;
            other.attachedRigidbody.position = otherPosition;
           // other.attachedRigidbody.position = playerController.introBurrowPoint;
        }
}
}
