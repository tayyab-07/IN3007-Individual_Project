using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.AI;

public class GuardBehaviourTree : BehaviorTree.Tree
{
    [Header("Patrol Points")]
    public Transform[] patrolPoints;

    [Header("Objects")]
    public LayerMask viewMask;
    public Transform player;
    public NavMeshAgent agent;
    public Light spotlight;
    public GuardBehaviourTree guard;

    [Header("Booleans")]
    public bool attackPlayer = false;
    public bool playerSeen = false;

    public bool conductAttack = false;
    public bool conductSearch = false;

    public bool search1 = false;
    public bool search2 = false;

    [Header("Timer")]
    public float timePlayerVisible;

    [Header("Zones")]
    public ZoneState zone;
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
            //new GuardOrganise(guard),

            new Sequence (new List<Node>
            { 
                new CheckEnemyZone(transform, player, viewMask, guard),

                new CheckEnemySpotted(spotlight, guard),

                new Selector(new List<Node>
                { 
                    new Sequence(new List<Node>
                    {
                        new CheckEnemyInAttackRange(guard),
                        new GuardAttack(spotlight),
                    }),

                    new GuardChase(player, agent),
                }),
            }),

            new GuardPatrol(transform, patrolPoints, agent),

        });

        return root;
    }
}
