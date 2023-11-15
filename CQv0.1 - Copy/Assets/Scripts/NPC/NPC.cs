using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject interactCanvas;
    public GameObject dialogueCanvas;

    public GameObject player;
  
    private void OnTriggerStay2D(Collider2D other)
    {
           PlayerController playerController = player.GetComponent<PlayerController>();
        //this works
        if (other.CompareTag("Player"))
        {
           // PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

             interactCanvas.SetActive(true);
           // Destroy(gameObject);
           //I tried to have it so only while isInteracting is true would the dialogue canvas work 
           //but because it runs multiple times(not sure why, prob need to fix that later)
           //but it also always works even when isinteracting is set to false. Maybe i'm missing something
           //for now, we'll just set it to the input. get button
            if (Input.GetButton("Interact"))
            {
                dialogueCanvas.SetActive(true);
            }
           
          
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
           dialogueCanvas.SetActive(false);
            //playerInventory.playerPistolAmmo += 6;
            //this line of code is working.
            Debug.Log("Player leaves interaction zone");
           // Destroy(gameObject);
           
          
        }
        else
        {
            return;

        }
    }
}
