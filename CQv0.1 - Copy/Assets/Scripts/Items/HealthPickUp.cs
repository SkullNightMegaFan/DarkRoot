using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatus playerStatus = other.GetComponent<PlayerStatus>();
           
           if (playerStatus != null)
           {
            playerStatus.playerHealth += 4;
            Debug.Log("A heart has been reestored");
            Destroy(gameObject);
           }
          
        }
        else
        {
            //////////
        }
    }

}
