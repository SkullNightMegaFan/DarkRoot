using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnemyDeath : MonoBehaviour
{
    
    public GameObject healthpickup;
    public GameObject ammoPickUp;
    private void OnEnable()
    {
        // int randomChance = Random.Range(1,100);
        // if (randomChance >= 50 )
        // {
        //     Instantiate(ammoPickUp);
        //     Debug.Log("Ammo drop");
        // }
        // else
        // {
        //     Instantiate(healthpickup);
        //     Debug.Log("Health drop");

        // }
        //Enemy.OnEnemyDeath += LootDrop; 
    }
    private void OnDisable()
    {
        //Enemy.OnEnemyDeath -= LootDrop;
    }




}

