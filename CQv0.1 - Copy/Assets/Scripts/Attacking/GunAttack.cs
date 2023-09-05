using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAttack : MonoBehaviour
{
    public GameObject bulletOriginPoint;
    public GameObject bulletType;

    protected internal Vector3 bulletSpawnLocation;
    protected internal Vector2 direction;

    void Update()
    {
        bulletSpawnLocation = bulletOriginPoint.transform.position;
        FindTarget();
    }

    virtual protected void FindTarget()
    {
        Vector3 mousePosition =  Camera.main.GetComponent<MouseBehaviour>().mousePosition;
        direction = (mousePosition - bulletSpawnLocation).normalized;
    }

    public void Attack()
    {
        SpawnBullet();
    }

    void SpawnBullet()
    {// Instantiate at position (x) and zero rotation.
        GameObject bullet = Instantiate(bulletType, bulletSpawnLocation, Quaternion.identity);
        bullet.GetComponent<BulletBehaviour>().velocity = direction;
    }
}
