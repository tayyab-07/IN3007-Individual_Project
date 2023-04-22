using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class GuardBehaviourTree : BehaviorTree.Tree
{
    public UnityEngine.Transform[] patrolPoints;

    public static LayerMask viewMask;
    public static Transform player;
    public static NavMeshAgent agent;
    public static Light spotlight;

    public static bool playerSeen = false;
    public static bool attackPlayer = false;

    public static ZoneState zone;
    public enum ZoneState
    {
        emptyZone,
        zone1,
        zone2,
        zone3,
        zone4,
        zone5
    }

    protected override Node SetupTree()
    {
        Node root = new GuardPatrol(transform, patrolPoints);
        return root;
    }
}
