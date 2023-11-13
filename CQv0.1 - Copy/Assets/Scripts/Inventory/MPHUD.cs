using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPHUD : MonoBehaviour {

    public GameObject player;
    private Text hudText;
	
	
	// Update is called once per frame
	void Update () {
      
         hudText = GetComponent<Text>();
        PlayerStatus playerStatus = player.GetComponent<PlayerStatus>();
        PlayerController playerController = player.GetComponent<PlayerController>();
        PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
        //playerAmmo = playerController.currentAmmo;
        //current ammo display & max ammo display
        int currAmmo = playerController.currentAmmo;
        int maxAmmo = playerController.maxAmmo;
        int playerPistolAmmo = playerInventory.playerPistolAmmo;
        //health display
        float playerHealth = playerStatus.playerHealth;

        hudText.text = "Ammo: " + currAmmo + "/" + playerPistolAmmo;

        //WorkingHUD();
        //hudText.text = "Ammo:" + playerController.currentAmmo.ToFloat();

    }

    // void WorkingHUD()
    // {
    //        hudText = GetComponent<Text>();
    //     PlayerStatus playerStatus = player.GetComponent<PlayerStatus>();
    //     PlayerController playerController = player.GetComponent<PlayerController>();
    //     PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
    //     //playerAmmo = playerController.currentAmmo;
    //     //current ammo display & max ammo display
    //     int currAmmo = playerController.currentAmmo;
    //     int maxAmmo = playerController.maxAmmo;
    //     int playerPistolAmmo = playerInventory.playerPistolAmmo;
    //     //health display
    //     float playerHealth = playerStatus.playerHealth;

    //     hudText.text = "Ammo: " + currAmmo + "/" + playerPistolAmmo;
    // }
}
