using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunestoneDisplay : MonoBehaviour
{

    public TMP_Text runeTitle;
    public TMP_Text runeText;

    RunestoneDatabase runeData;


    // Start is called before the first frame update
    void Start()
    {
        runeData = GetComponent<RunestoneDatabase>();

    }

    public void SpawnDisplayText(string runestoneName)
    {
        Debug.Log("spawn display function triggered");

        runeTitle.text = runeData.GetRuneTitle(runestoneName);
        runeText.text = runeData.GetRuneText(runestoneName);

        this.gameObject.SetActive(true);
    }
    public void HideDisplay()
    {
        Debug.Log("hide display function triggered");

        this.gameObject.SetActive(false);

    }
}
