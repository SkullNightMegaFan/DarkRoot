using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadUIScript : MonoBehaviour
{

    private Text reloadText;
    public GameObject player;
    
    // Update is called once per frame
   
	
	// Update is called once per frame
	void Update () {
      
         reloadText = GetComponent<Text>();
        PlayerController playerController = player.GetComponent<PlayerController>();
        PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
        //playerAmmo = playerController.currentAmmo;
        //current ammo display & max ammo display
         int currAmmo = playerController.currentAmmo;
         int maxAmmo = playerController.maxAmmo;
         int playerPistolAmmo = playerInventory.playerPistolAmmo;
        bool reloadTrue = playerController.isReloading;
        if (reloadTrue == true)
        {

            
            reloadText.text = "Reloading!";
            //StartCoroutine(TextflashColor());
             if (currAmmo == 0 && playerPistolAmmo > 0)
             {
            // reloadText.text = "Reloading!";
             }
           else if (currAmmo == 0 && playerPistolAmmo == 0)
            {
            // reloadText.text = "Reloading!";
            }

        }
        else  
        {
            reloadText.text = " ";
               if (currAmmo == 0 && playerPistolAmmo > 0)
             {
            reloadText.text = "Reload!";
             }
           else if (currAmmo == 0 && playerPistolAmmo == 0)
            {
            reloadText.text = "Find Bullets!";
            }
            
        }
        
       


        // if (currAmmo == 0 && playerPistolAmmo > 0)
        // {
        //     Debug.Log(currAmmo);
        //     reloadText.text = "Reload!";
        // }
        // else if (currAmmo == 0 && playerPistolAmmo == 0)
        // {
        //     reloadText.text = "Find Bullets";
        //     //need to changet the appearance of the text, change 
        // }
        // else  
        // {
        //     reloadText.text = " ";
        // }
        
        IEnumerator TextflashColor()
        {
        reloadText.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        

        }
     
    }
}
