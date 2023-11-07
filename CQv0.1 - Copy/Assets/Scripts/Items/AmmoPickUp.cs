using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
 private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
           
           
            playerInventory.playerPistolAmmo += 6;
            Debug.Log("Ammo has been reestored");
            Destroy(gameObject);
           
          
        }
        else
        {
            //////////
        }
    }

}
