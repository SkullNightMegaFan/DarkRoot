using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedEnemyAggro : EnemyAggro
{


    protected override void EnterAggro()
    {
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

    }
    protected override void ExitAggro()
    {
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
    }

 
}
