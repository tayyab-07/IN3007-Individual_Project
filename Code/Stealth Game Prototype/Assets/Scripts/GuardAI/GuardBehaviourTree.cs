using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardBehaviourTree : BehaviorTree.Tree
{
    public UnityEngine.Transform[] patrolPoints;

    public LayerMask viewMask;
    public Transform player;
    public NavMeshAgent agent;
    public Light spotlight;

    public static bool attackPlayer = false;
    public static bool playerSeen = false;
    public static bool playerVisible = false;

    public static bool conductAttack = false;
    public static bool conductSearch = false;

    public static bool search1 = false;
    public static bool search2 = false;    

    public static float timePlayerVisible;

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

        Node root = new Selector(new List<Node>
        {   
            new Sequence (new List<Node>
            { 
                new CheckEnemyInAttackRange(),
                new GuardAttack(transform),
            }),

            new Sequence (new List<Node>
            { 
                new CheckEnemyZone(transform, player, viewMask),
                new CheckEnemySpotted(spotlight),
                new GuardChase(player, agent),
            }),

            new GuardOrganise(),

            new GuardPatrol(transform, patrolPoints, agent),

        }) ;

        return root;
    }
}
