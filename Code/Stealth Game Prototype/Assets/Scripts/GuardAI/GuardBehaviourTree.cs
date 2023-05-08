using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public AlertedSprite alertedSprite;
    public SearchingSprite searchingSprite;

    [Header("Booleans")]
    public bool attackPlayer = false;
    public bool playerSeen = false;
    public bool playerVisible = false;

    public bool search1 = false;
    public bool search2 = false;

    [Header("Timer")]
    public float timePlayerVisible;

    [Header("Organise")]
    public bool organiseAttack;
    public bool organiseSearch;

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
        // Structure for guard behaviour tree

        Node root = new Selector(new List<Node>
        {
            new Sequence (new List<Node>
            {
                new CheckAttack(guard),
                new GroupAttack(player, guard, agent),
            }),

            new Sequence (new List<Node>
            { 
                new CheckEnemyZone(transform, player, viewMask, guard),

                new CheckEnemySpotted(spotlight, guard, alertedSprite, searchingSprite),

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
            
            new Sequence (new List<Node>
            { 
                new CheckSearch(guard),
                new GroupSearch(alertedSprite, searchingSprite),
            }),

            new GuardPatrol(transform, patrolPoints, agent, guard, alertedSprite, searchingSprite),

        });

        return root;
    }
}
