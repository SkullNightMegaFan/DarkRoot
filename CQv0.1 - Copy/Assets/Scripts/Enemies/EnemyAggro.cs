using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAggro : MonoBehaviour
{
    protected internal Enemy enemy;

    private void Start()
    {
        enemy = this.transform.parent.GetComponent<Enemy>();
        AIPath aiPath = this.transform.parent.GetComponent<AIPath>();
        aiPath.canMove = false; enemy.followingPlayer = false;
    }

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        switch (other.gameObject.layer)
        {
            case 6:
                //  Player layer
                FlockerScript flocking = this.transform.parent.GetComponent<FlockerScript>();
                AIPath aiPath = this.transform.parent.GetComponent<AIPath>(); 
                if (flocking != null)
                {
                    flocking.SwitchFlockingMode(FlockerScript.FlockingMode.ChaseTarget);
                    enemy.followingPlayer = true;

                }
                if (aiPath != null)
                {
                    aiPath.canMove = true;
                    enemy.followingPlayer = true;

                }

                break;
            case 7:
                //  Enemies layer
                break;
            case 8:
                //  Projectiles layer: ignore for now
                break;
            default:
                break;
        }

    }

}
