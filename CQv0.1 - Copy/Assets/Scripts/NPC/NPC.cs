using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject interactCanvas;
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();
        //this works
        if (other.CompareTag("Player"))
        {
           // PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
           //canvas is not being enabled when this line of code
           interactCanvas.SetActive(true);
            //playerInventory.playerPistolAmmo += 6;
            //this line of code is working.
            Debug.Log("Player enters interaction zone");
           // Destroy(gameObject);

           
          
        }
        else
        {
            interactCanvas.SetActive(false);

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
                PlayerController playerController = player.GetComponent<PlayerController>();

         if (other.CompareTag("Player"))
        {
           // PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();
           //canvas is not being enabled when this line of code
           interactCanvas.SetActive(false);
            //playerInventory.playerPistolAmmo += 6;
            //this line of code is working.
            Debug.Log("Player leaves interaction zone");
           // Destroy(gameObject);
           
          
        }
        else
        {
            interactCanvas.SetActive(false);

        }
    }
}
