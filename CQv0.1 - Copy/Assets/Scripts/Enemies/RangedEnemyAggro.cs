using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class RangedEnemyAggro : EnemyAggro
{


    protected override void EnterAggro()
    {
        Enemy_BulletBunny bulletBunny = this.transform.parent.GetComponent<Enemy_BulletBunny>();
        FlockerScript flocking = this.transform.parent.GetComponent<FlockerScript>();
        AIPath aiPath = this.transform.parent.GetComponent<AIPath>();

        if (flocking != null)
        {
            flocking.SwitchFlockingMode(FlockerScript.FlockingMode.MaintainDistance);
            enemy.followingPlayer = true;
            bulletBunny.isAggroed = true;
        }
        if (aiPath != null)
        {
            aiPath.canMove = true;
            enemy.followingPlayer = true;
            bulletBunny.isAggroed = true;

        }

    }
    protected override void ExitAggro()
    {
        Enemy_BulletBunny bulletBunny = this.transform.parent.GetComponent<Enemy_BulletBunny>();
        FlockerScript flocking = this.transform.parent.GetComponent<FlockerScript>();
        AIPath aiPath = this.transform.parent.GetComponent<AIPath>();
        if (flocking != null)
        {
            flocking.SwitchFlockingMode(FlockerScript.FlockingMode.DoNothing);
            enemy.followingPlayer = false;
            bulletBunny.isAggroed = false;

        }
        if (aiPath != null)
        {
            aiPath.canMove = false;
            enemy.followingPlayer = false;
            bulletBunny.isAggroed = false;

        }
    }

 
}
