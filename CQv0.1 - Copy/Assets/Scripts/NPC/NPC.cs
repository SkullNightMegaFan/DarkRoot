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
           //Post vertical slice, I'll have the dialogue text to read from a text file and
           //every time the player presses e it will have them go through the next blurb. 
           Text dialogueText = dialogueCanvas.GetComponentInChildren<Text>();
        //this works
        if (other.CompareTag("Player"))
        {
           // PlayerInventory playerInventory = other.GetComponent<PlayerInventory>();

             interactCanvas.SetActive(true);
             //will get around to mapping it to the action key
            if (playerController.isInteracting == true)
            {
                dialogueCanvas.SetActive(true);
                

                dialogueText.text = "Press E";
                //A coroutine will start and then immediately go to the next line of code
                // it does not need to be finished in order for the next line of code to be fun. 
                 StartCoroutine(MiniWait());
                
               // StartCoroutine(MiniWait());

                //eventually this will be using the new input manager system but for now this will do.
                //in addition, i want the dialogue text, to be read from a txt file. 
              //   if (Input.GetKey(KeyCode.E))
                
            }   
          
        }
        else
        {
            interactCanvas.SetActive(false);

        }
       // if (playerController.isTalking == true)
        if (playerController.isTalking == true && dialogueCanvas.activeSelf == true)
        {
             //if (Input.GetKey(KeyCode.Space))
                             if (Input.GetKey(KeyCode.E))

                    {
                        Debug.Log("We can talk");
                        dialogueText.text = "This is the second dialogue";
                    }
               // else if (Input.GetKeyDown(KeyCode.Space))
               else if (Input.GetMouseButtonDown(1))
                {
                //     dialogueCanvas.SetActive(false);
                //        playerController.UnlockMovement();
                //       playerController.canDodge = true;
                //  playerController.canShoot = true;
                //  playerController.isInvincible = false;
                playerController.isTalking = false;
                Debug.Log("The B has been pressed");
                }
        }
    }

    IEnumerator MiniWait()
    {
                   PlayerController playerController = player.GetComponent<PlayerController>();

    yield return new WaitForSeconds(0.3f);
    playerController.isTalking = true;
    Debug.Log("The player can continue talking");
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

           
          
        }
        else
        {
            return;

        }
    }


}
