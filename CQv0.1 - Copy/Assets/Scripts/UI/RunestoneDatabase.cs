using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RuneDataEntry
{
    public string runestoneName;
    public string runeTitle;
    public string runeText;
}
[System.Serializable]
public class RunestoneDataJSON
{
    public RuneDataEntry[] Runes;
}

public class RunestoneDatabase : MonoBehaviour
{
    public TextAsset runeJsonData;
    RunestoneDataJSON runeData;

    // Start is called before the first frame update
    void Start()
    {
        string json = runeJsonData.text;
        runeData = JsonUtility.FromJson<RunestoneDataJSON>(json);
    }

    public string GetRuneTitle(string runestoneName)
    {
        for (int i = 0; i < runeData.Runes.Length; ++i)
        {
            if(runeData.Runes[i].runestoneName == runestoneName)
            {
                return runeData.Runes[i].runeTitle;
            }
        }
        return "[NO RUNESTONE TITLE FOUND]";
    }
    public string GetRuneText(string runestoneName)
    {
        for (int i = 0; i < runeData.Runes.Length; ++i)
        {
            if (runeData.Runes[i].runestoneName == runestoneName)
            {
                return runeData.Runes[i].runeText;
            }
        }
        return "[NO RUNESTONE TEXT FOUND]";
    }



}
