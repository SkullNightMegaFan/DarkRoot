using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject interactCanvas;
    public GameObject dialogueCanvas;

    public GameObject player;
    
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
            if (playerController.isInteracting == true && playerController.isTalking == false)
            {
                //vertical slice implementation
                    //when the player is talking all enemies OnFlock script should change to teh DoNothing behavior. 

                dialogueCanvas.SetActive(true);
                

                dialogueText.text = "Strange to see someone traveling alone these days....";
                //post verticle slice i should just create a dialogue tree coroutine and go from there. 
                //not sure how i would do that with the return values needed but if there's a will there's a way. 
                StartCoroutine(MiniWait());
                //A coroutine will start and then immediately go to the next line of code
                // it does not need to be finished in order for the next line of code to be fun. 
                //playerController.Talking();
               // StartCoroutine(MiniWait());
                //eventually this will be using the new input manager system but for now this will do.
                //in addition, i want the dialogue text, to be read from a txt file. 
              //   if (Input.GetKey(KeyCode.E))
                
            }   
            else
            {
                //
            }
          
        }
        else
        {
            interactCanvas.SetActive(false);

        }

        if (playerController.isTalking == true  && playerController.isInteracting == true)//dialogueCanvas.activeSelf == true)
        //if (playerController.isTalking == true && )
        {
             //if (Input.GetKey(KeyCode.Space))
                          
                dialogueText.text = "Be safe on your travels.....vagabond.";
                playerController.StoppedTalking();
                     

                    
               // else if (Input.GetKeyDown(KeyCode.Space))
            //    else if (Input.GetMouseButtonDown(0))
            //     {
            //     //     dialogueCanvas.SetActive(false);
            //     //        playerController.UnlockMovement();
            //     //       playerController.canDodge = true;
            //     //  playerController.canShoot = true;
            //     //  playerController.isInvincible = false;
            //     playerController.isTalking = false;
            //     Debug.Log("The B has been pressed");
            //     }
        }
    }

    IEnumerator MiniWait()
    {
                   PlayerController playerController = player.GetComponent<PlayerController>();

    yield return new WaitForSeconds(0.3f);
        playerController.Talking();


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
           
          
        }
        else
        {
            return;

        }
    }


}
