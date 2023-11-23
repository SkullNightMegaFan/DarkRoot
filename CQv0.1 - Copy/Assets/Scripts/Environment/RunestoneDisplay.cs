using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunestoneDisplay : MonoBehaviour
{
    public string runestoneName;
    string currentRune;

    GameObject display;
    Canvas canvas;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject tempObject = GameObject.Find("RunestoneCanvas");   // find the canvas to spawn display in
        if (tempObject != null)
        {
            //If we found the object , get the Canvas component from it.
            canvas = tempObject.GetComponent<Canvas>();
            display = canvas.transform.GetChild(0).gameObject;
            if (canvas == null)
            {
                Debug.Log("Could not locate Canvas component on " + tempObject.name);
            }
        }

    }

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        switch (other.gameObject.layer)
        {
            case 6:  //  Player layer
                currentRune = runestoneName;
                SpawnDisplayText();
                break;
        }
    }
    virtual protected void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        switch (other.gameObject.layer)
        {
            case 6:  //  Player layer
                HideDisplay();
                break;
        }
    }

    void SpawnDisplayText()
    {
        display.SetActive(true);
    }
    void HideDisplay()
    {
        display.SetActive(false);
    }

}
