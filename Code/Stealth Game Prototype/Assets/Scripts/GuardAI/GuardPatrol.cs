using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class GuardPatrol : Node
{
    private Transform _guardTransform;
    private Transform[] _patrolPoints;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int currentPatrolPoint;
    private float patrolSpeed = 2.0f;
    private float patrolStopDuration = 2.5f;
    private float patrolStopTimer = 0.0f;
    private bool stopped = false;
    private float patrolRotationSpeed = 90.0f;

    public GuardPatrol(Transform guardTransform, Transform[] patrolPoints)
    { 
        _guardTransform = guardTransform;
        _patrolPoints = patrolPoints;
    }

    public override NodeState Evaluate()
    {
        // Method to set the patrol path of the guard

        Transform wp = _patrolPoints[currentPatrolPoint];

        // if the guard is waiting along thier patrol path, start a timer and rotate the guard to face the next waypoint
        if (stopped == true)
        {
            patrolStopTimer = patrolStopTimer + Time.deltaTime;
            if (patrolStopTimer < patrolStopDuration)
            {
                Vector3 targetDirection = (wp.position - _guardTransform.position).normalized;  //[1]
                float angleToTarget = 90 - Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg;  //[1]

                if (Mathf.Abs(Mathf.DeltaAngle(_guardTransform.eulerAngles.y, angleToTarget)) > 0.05f)  //[1]
                {
                    float angle = Mathf.MoveTowardsAngle(_guardTransform.eulerAngles.y, angleToTarget, patrolRotationSpeed * Time.deltaTime); //[1]
                    _guardTransform.eulerAngles = Vector3.up * angle; //[1]
                }
            }

            else
            { 
                stopped = false;
            }
        }

        else
        {
            // if guard reaches a waypoint set their target to the next waypoint
            if (Vector3.Distance(_guardTransform.position, wp.position) < 0.1f)
            {
                _guardTransform.position = wp.position;
                patrolStopTimer = 0.0f;
                stopped = true;

                currentPatrolPoint = (currentPatrolPoint + 1) % _patrolPoints.Length;
            }
            // otherwise keep looking at the current waypoint
            else
            {
                _guardTransform.position = Vector3.MoveTowards(_guardTransform.position, wp.position, patrolSpeed * Time.deltaTime);
                if (Vector3.Distance(_guardTransform.position, wp.position) > 2.0f)
                {
                    _guardTransform.LookAt(wp.position);
                }
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
