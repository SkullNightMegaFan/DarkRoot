using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public GameObject heartPrefab;
    public PlayerStatus playerStatus;
    List<HealthHeart> hearts = new List<HealthHeart>();

    private void OnEnable()
    {
        PlayerStatus.OnPlayerDamaged += DrawHearts;  //adds function to OnPlayerDamagedEvent ?
    }
    private void OnDisable()
    {
        PlayerStatus.OnPlayerDamaged -= DrawHearts;  
    }

    private void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();

        float maxHealthRemainder = playerStatus.maxHealth % 2;   //determines if maxHealth is odd or even, returns 0 or 1
        int heartsToMake = (int)((playerStatus.maxHealth / 2) + maxHealthRemainder); //divides health in 2 to determine how many hearts

        for (int i = 0; i < heartsToMake; i++)  // for as long as there are heartsToMake
        {
            CreateEmptyHeart();  //make an empty heart, add it to the hearts list
        }

        for (int i = 0; i < hearts.Count; i++) //for all the hearts that have been made
        {
            int heartStatusRemainder = (int)Mathf.Clamp(playerStatus.Health - (i * 2), 0, 2); //there are heart statuses 0, 1, and 2 
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);  // for all hearts in the list, set the image to match state 0, 1, or 2
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent); //add new heart to the hearts list
    }

    public void ClearHearts()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }

}
