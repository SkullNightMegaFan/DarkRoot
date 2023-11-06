using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPHUD : MonoBehaviour {

    public PlayerStatus playerStatus;
    public PlayerController playerController;
    Text hudText;
	// Use this for initialization
	void Start () {
        hudText = GetComponent<Text>();
        //playerAmmo = playerController.currentAmmo;
	}
	
	// Update is called once per frame
	void Update () {
        hudText.text = "Ammo: " + playerController.dodgeForce + "/" + playerStatus.playerHealth;// + "/" + playerStatus.MaxMP;
        //hudText.text = "Ammo:" + playerController.currentAmmo.ToFloat();

    }
}
