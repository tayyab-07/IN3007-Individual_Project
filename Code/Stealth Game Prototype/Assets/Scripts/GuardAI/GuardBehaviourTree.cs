using BehaviorTree;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardBehaviourTree : BehaviorTree.Tree
{
    [Header("Patrol Points")]
    public Transform[] patrolPoints;

    [Header("Objects")]
    public LayerMask obstacleMask;
    public LayerMask smokeMask;
    public Transform player;
    public NavMeshAgent agent;
    public Light spotlight;
    public GuardBehaviourTree guard;
    public Transform smoke;

    [Header("Sprites")]
    public AlertedSprite alertedSprite;
    public SearchingSprite searchingSprite;
    public DetectionBarSprite detectionBarSprite;

    [Header("Booleans")]
    public bool attackPlayer = false;
    public bool playerSeen = false;
    public bool playerVisible = false;

    public bool search1 = false;
    public bool search2 = false;

    public bool smokeVisible = false;
    public bool smokeSeen = false;

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

    public void Start()
    {
        smoke = GameObject.FindGameObjectWithTag("Smoke").transform;
    }

    protected override Node SetupTree()
    {
        // Structure for guard behaviour tree

        Node root = new Selector(new List<Node>
        {

            new Sequence (new List<Node>
            { 
                new CheckSmoke(transform, agent, smoke, obstacleMask, guard),
                new GuardSmokeResponse(guard),
            }),

            new Sequence (new List<Node>
            {
                new CheckAttack(guard),
                new GroupAttack(player, guard, agent),
            }),

            new Sequence (new List<Node>
            { 
                new CheckEnemyZone(transform, player, obstacleMask, smokeMask, guard),

                new CheckEnemySpotted(spotlight, guard, alertedSprite, searchingSprite, detectionBarSprite),

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
