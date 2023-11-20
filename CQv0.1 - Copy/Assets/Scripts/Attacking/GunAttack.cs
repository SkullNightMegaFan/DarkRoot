using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunAttack : MonoBehaviour
{
    public GameObject bulletOriginPoint;
    public GameObject bulletType;

    protected internal Vector3 bulletSpawnLocation;
    protected internal Vector2 direction;

    private InputAction fire;

    void Update()
    {
        bulletSpawnLocation = bulletOriginPoint.transform.position;
        FindTarget();
    }

//finds the cursor and then puts the direction of the cursor
    virtual protected void FindTarget()
    {
        Vector3 mousePosition =  Camera.main.GetComponent<MouseBehaviour>().mousePosition;
        direction = (mousePosition - bulletSpawnLocation).normalized;
    }

//this is what spawns the bullet
    public void Attack()
    {
        SpawnBullet();
    }

//personally with this, I think I should put ammo and reload time of the gun here. 
//or at least within this script and have the player controller read the variables from the script. 
//Alas, time is an ever marching dictator. I'll leave the comment here so future
// team members or just me can get to work on it
    void SpawnBullet()
    {// Instantiate at position (x) and zero rotation.
        GameObject bullet = Instantiate(bulletType, bulletSpawnLocation, Quaternion.identity);
        bullet.GetComponent<BulletBehaviour>().velocity = direction;
    }
}
