using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject interactCanvas;
    public GameObject dialogueCanvas;

    public GameObject player;
    private int dialogueCounter;
    
    //add list of all enemies
  
    private void OnTriggerStay2D(Collider2D other)
    {
           PlayerController playerController = player.GetComponent<PlayerController>();
           //Post vertical slice, I'll have the dialogue text to read from a text file and
           //every time the player presses e it will have them go through the next blurb. 
           Text dialogueText = dialogueCanvas.GetComponentInChildren<Text>();
        //this works
        if (other.CompareTag("Player"))
        {

             interactCanvas.SetActive(true);
             //will get around to mapping it to the action key
            if (interactCanvas.activeInHierarchy == true && playerController.isInteracting)
            {
                //vertical slice implementation
                    //when the player is talking all enemies OnFlock script should change to teh DoNothing behavior. 
                dialogueCounter++;
                StartCoroutine(MiniWait());


            if (dialogueCounter == 1)
            {
                playerController.isTalking = true;
                dialogueCanvas.SetActive(true);
                

                dialogueText.text = "Strange to see someone traveling alone these days....";
            }
            else if (dialogueCounter == 2)
            {
                dialogueText.text = "Be safe on your travels.....vagabond.";
                
            }
            }   
            else if (dialogueCounter >= 3)
            {
                playerController.StoppedTalking();
                dialogueCanvas.SetActive(false);

            }
          
        }
        else
        {
            interactCanvas.SetActive(false);

        }

    }

IEnumerator MiniWait()
{yield  return new WaitForSeconds(0.5f);

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
           //I think this allows player to exit an NPC's interact area and get free dodge cool down. 
           //Sounds cool i'll keep it in
            playerController.StoppedTalking();
            dialogueCounter = 0;
           
          
        }
        else
        {
            return;

        }
    }


}
