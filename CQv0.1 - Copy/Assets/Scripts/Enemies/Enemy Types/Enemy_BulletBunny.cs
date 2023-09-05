using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BulletBunny : Enemy
{
    private Vector2 direction;
    [SerializeField] GameObject gun;
    [SerializeField] float fireRate = 1f;
    private Enemy_GunAttack gunAttack;
    private bool isFiringGun = false;

    private void FixedUpdate()
    {
        Vector3 rotationAnchorPosition = transform.position;
        Vector3 playerPosition = player.transform.position;
        LookToPlayer(playerPosition, rotationAnchorPosition);
    }

    protected override void ChaseTarget()
    {
        this.GetComponent<FlockerScript>().SwitchFlockingMode(FlockerScript.FlockingMode.MaintainDistance);


    }

    void LookToPlayer(Vector3 target, Vector3 anchor)
    {
        direction = target - anchor;
        if (followingPlayer)
        {
            float angle = Vector2.SignedAngle(Vector2.down, direction);
            transform.eulerAngles = new Vector3(0, 0, angle);

            StartCoroutine(EnemyShoot());
        }
    }
    IEnumerator EnemyShoot()
    {
        yield return new WaitForSeconds(1f);
        if (!isFiringGun)
        {
            isFiringGun = true;
            gunAttack = gun.GetComponent<Enemy_GunAttack>();
            if (gunAttack != null)
            {
                gunAttack.Attack();
                yield return new WaitForSeconds(fireRate);
                isFiringGun = false;
            }
            else
            {
                Debug.Log("No gunAttack script found");
            }
        }
    }

}


