using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneTrigger : MonoBehaviour
{
    public string runestoneName;
    RunestoneDisplay runeDisplay;

    private void Start()
    {
        runeDisplay = GameObject.Find("RunestoneDisplay").GetComponent<RunestoneDisplay>();

    }


    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        switch (other.gameObject.layer)
        {
            case 6:  //  Player layer
                runeDisplay.SpawnDisplayText(runestoneName);
                break;
        }
    }
    virtual protected void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        switch (other.gameObject.layer)
        {
            case 6:  //  Player layer
                runeDisplay.HideDisplay();
                break;
        }
    }
}
