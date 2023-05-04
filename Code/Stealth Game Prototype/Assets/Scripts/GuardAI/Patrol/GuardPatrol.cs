using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class GuardPatrol : Node
{
    private Transform _guardTransform;
    private Transform[] _patrolPoints;
    private NavMeshAgent _agent;

    private int currentPatrolPoint;
    private float patrolStopDuration = 2.5f;
    private float patrolStopTimer = 0.0f;
    private bool stopped = false;

    public GuardPatrol(Transform guardTransform, Transform[] patrolPoints, NavMeshAgent agent)
    { 
        _guardTransform = guardTransform;
        _patrolPoints = patrolPoints;
        _agent = agent;
    }

    public override NodeState Evaluate()
    {
        // Method to set the patrol path of the guard

        Transform wp = _patrolPoints[currentPatrolPoint];

        // if the guard is waiting along thier patrol path, start a timer and rotate the guard to face the next waypoint
        if (stopped == true)
        {
            patrolStopTimer = patrolStopTimer + Time.deltaTime;
            if (patrolStopTimer > patrolStopDuration)
            {
                stopped = false;
            }
        }

        else if (stopped == false)
        {
            // if guard reaches a waypoint set their target to the next waypoint
            if (Vector3.Distance(_guardTransform.position, wp.position) < (_agent.stoppingDistance + 0.2f))
            {
                patrolStopTimer = 0.0f;
                stopped = true;

                currentPatrolPoint = (currentPatrolPoint + 1) % _patrolPoints.Length;
            }
            else
            {
                _agent.SetDestination(wp.position);
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
