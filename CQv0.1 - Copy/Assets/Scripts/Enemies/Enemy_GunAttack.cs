using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GunAttack : GunAttack
{
    private GameObject player;

    protected override void FindTarget()
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        direction = (playerPosition - bulletSpawnLocation).normalized;
    }

}
