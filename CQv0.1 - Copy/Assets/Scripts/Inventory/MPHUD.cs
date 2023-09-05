using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPHUD : MonoBehaviour {

    public PlayerStatus playerStatus;
    Text hudText;

	// Use this for initialization
	void Start () {
        hudText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
      //  hudText.text = "MP: " + playerStatus.GetCurrentMP() + "/" + playerStatus.MaxMP;
    }
}
