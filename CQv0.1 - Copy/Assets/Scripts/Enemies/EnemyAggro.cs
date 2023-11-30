using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAggro : MonoBehaviour
{
    protected internal Enemy enemy;
    float distance;
    bool aggroed;

    private void Start()
    {
        aggroed = false;
        enemy = this.transform.parent.GetComponent<Enemy>();
        AIPath aiPath = this.transform.parent.GetComponent<AIPath>();
        aiPath.canMove = false; enemy.followingPlayer = false;
        distance = Vector3.Distance(this.transform.position, aiPath.destination);
    }

    virtual protected void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Collision with " + other.gameObject.name);
        switch (other.gameObject.layer)
        {
            case 6:
                //  Player layer
                EnterAggro();
                aggroed = true;
                break;

                /*
            case 7:
                //  Enemies layer
                break;
            case 8:
                //  Projectiles layer: ignore for now
                break;
            default:
                break;
                */
        }

    }

    private void FixedUpdate()
    {
        if (aggroed == true)
        {   //Debug.Log("aggroed is true");

            AIPath aiPath = this.transform.parent.GetComponent<AIPath>();
            distance = Vector3.Distance(this.transform.position, aiPath.destination);
            //Debug.Log("Remaining distance is " + distance);

            if (distance >= 3.8)
            {
                //Debug.Log("Exiting aggro");
                ExitAggro();
                aggroed = false;
            }
        }
    }

    virtual protected void EnterAggro()
    {
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
    }
    virtual protected void ExitAggro ()
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
