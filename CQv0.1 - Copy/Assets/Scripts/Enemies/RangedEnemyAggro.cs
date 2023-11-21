using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedEnemyAggro : EnemyAggro
{


    protected override void OnTriggerEnter2D(Collider2D other)
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
                    flocking.SwitchFlockingMode(FlockerScript.FlockingMode.MaintainDistance);
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

    virtual protected void OnTriggerExit2D(Collider2D other)
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
                    flocking.SwitchFlockingMode(FlockerScript.FlockingMode.DoNothing);
                    enemy.followingPlayer = false;

                }
                if (aiPath != null)
                {
                    aiPath.canMove = false;
                    enemy.followingPlayer = false;

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
