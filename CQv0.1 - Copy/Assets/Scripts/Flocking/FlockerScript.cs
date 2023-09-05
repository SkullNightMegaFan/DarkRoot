using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlockerScript : MonoBehaviour {

    public enum FlockingMode
    {
        ChaseTarget,
        FleeTarget,
        MaintainDistance,
        DoNothing
    }
    Rigidbody2D rb;
    public float SpeedPerSecond = 1.0f;
    GameObject FlockingTarget;
    public FlockingMode CurrentFlockingMode = FlockingMode.ChaseTarget;
    public float DesiredDistanceFromTarget_Min = 3.5f;
    public float DesiredDistanceFromTarget_Max = 4.5f;

    public bool avoidHazards = true;
    public bool avoidAllies = true;
    public Vector3 desiredDirection;
    public Vector3 movementDirection;


    private void OnEnable()
    {
        PlayerStatus.OnPlayerDeath += FlockingModeDeath;  //adds function to OnPlayerDamagedEvent ?

    }
    private void OnDisable()
    {
        PlayerStatus.OnPlayerDeath -= FlockingModeDeath;
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        FlockingTarget = GameObject.Find("Player");
    }

    void FixedUpdate () {

        desiredDirection = new Vector3();

        Vector3 vectorToTarget = FlockingTarget.transform.position - transform.position;
        float distanceToTarget = vectorToTarget.magnitude;
        
        switch (CurrentFlockingMode)
        {
            case FlockingMode.ChaseTarget:
                desiredDirection = vectorToTarget;          //Move towards target
                break;
            case FlockingMode.FleeTarget:
                desiredDirection = -vectorToTarget;          //Move away from target
                break;
            case FlockingMode.MaintainDistance:
                {
                    //If distance to target is less than DesiredDistanceFromTarget_Min, move away from target
                    //Otherwise, if distance to target is more than DesiredDistanceFromTarget_Max, move towards target

                    if (distanceToTarget < DesiredDistanceFromTarget_Min)
                    {
                        desiredDirection = -vectorToTarget;
                    }
                    else if (distanceToTarget > DesiredDistanceFromTarget_Max)
                    {
                        desiredDirection = vectorToTarget;
                    }
                }
                break;
            case FlockingMode.DoNothing:
                desiredDirection = Vector3.zero;
                break;
        }

        if(avoidHazards)
        {
            HazardScript[] hazards = FindObjectsOfType<HazardScript>();

            Vector3 avoidanceVector = Vector3.zero;
            for (int i = 0; i < hazards.Length; ++i)
            {
                Vector3 vectorToHazard = hazards[i].transform.position - transform.position;
                if (vectorToHazard.magnitude < 4.0f)
                {
                    Vector3 vectorAwayFromHazard = -vectorToHazard;
                    //Accumulate vectors away from hazards in avoidance vector
                    avoidanceVector += vectorAwayFromHazard;
                }
            }

            if(avoidanceVector != Vector3.zero)
            {
                desiredDirection.Normalize();
                avoidanceVector.Normalize();

                //Set the value of desiredDirection to 50% desiredDirection and 50% avoidanceVector
                desiredDirection = desiredDirection * 0.8f + avoidanceVector * 0.2f;
            }
        }
        if (avoidAllies)
        {
            Enemy[] allies = FindObjectsOfType<Enemy>();

            Vector3 avoidanceVector = Vector3.zero;
            for (int i = 0; i < allies.Length; ++i)
            {
                Vector3 vectorToHazard = allies[i].transform.position - transform.position;
                if (vectorToHazard.magnitude < 0.3f)
                {
                    Vector3 vectorAwayFromHazard = -vectorToHazard;
                    //Accumulate vectors away from hazards in avoidance vector
                    avoidanceVector += vectorAwayFromHazard;
                }
            }

            if (avoidanceVector != Vector3.zero)
            {
                desiredDirection.Normalize();
                avoidanceVector.Normalize();

                //Set the value of desiredDirection to 50% desiredDirection and 50% avoidanceVector
                desiredDirection = desiredDirection * 0.6f + avoidanceVector * 0.4f;
            }
        }

        desiredDirection.Normalize();
        rb.MovePosition(rb.position + (Vector2)desiredDirection * SpeedPerSecond * Time.fixedDeltaTime);


    }
    IEnumerator ModeDoNothing()
    {
        yield return new WaitForSeconds(1.8f);

        FlockingMode currentFlockingMode = this.CurrentFlockingMode;
        switch (currentFlockingMode)
        {
            case FlockingMode.ChaseTarget:
                currentFlockingMode = FlockingMode.DoNothing;
                CurrentFlockingMode = currentFlockingMode;
                break;
            case FlockingMode.FleeTarget:
                currentFlockingMode = FlockingMode.DoNothing;
                CurrentFlockingMode = currentFlockingMode;
                break;
            case FlockingMode.MaintainDistance:
                currentFlockingMode = FlockingMode.DoNothing;
                CurrentFlockingMode = currentFlockingMode;
                break;
            case FlockingMode.DoNothing:
                break;
        }
    }
    public void FlockingModeChase()
    {
        SwitchFlockingMode(FlockingMode.ChaseTarget);
    }
    public void FlockingModeDeath()
    {
        FlockingMode currentFlockingMode = this.CurrentFlockingMode;

        switch (currentFlockingMode)
        {
            case FlockingMode.ChaseTarget:
                currentFlockingMode = FlockingMode.MaintainDistance;
                CurrentFlockingMode = currentFlockingMode;
                break;
            case FlockingMode.FleeTarget:
                currentFlockingMode = FlockingMode.DoNothing;
                CurrentFlockingMode = currentFlockingMode;
                break;
            case FlockingMode.MaintainDistance:
                currentFlockingMode = FlockingMode.DoNothing;
                CurrentFlockingMode = currentFlockingMode;
                break;
            case FlockingMode.DoNothing:
                break;
        }
        StartCoroutine(ModeDoNothing());

    }
    public void SwitchFlockingMode(FlockingMode newMode)
    {
        FlockingMode currentFlockingMode = this.CurrentFlockingMode;

            if (newMode == FlockingMode.ChaseTarget)
        {
            currentFlockingMode = FlockingMode.ChaseTarget;
            CurrentFlockingMode = currentFlockingMode;
        }
        else if (newMode == FlockingMode.FleeTarget)
        {
            currentFlockingMode = FlockingMode.FleeTarget;
            CurrentFlockingMode = currentFlockingMode;
        }
        else if (newMode == FlockingMode.MaintainDistance)
        {
            currentFlockingMode = FlockingMode.MaintainDistance;
            CurrentFlockingMode = currentFlockingMode;
        }
        else if (newMode == FlockingMode.DoNothing)
        {
            currentFlockingMode = FlockingMode.DoNothing;
            CurrentFlockingMode = currentFlockingMode;
        }

    }
   
}
