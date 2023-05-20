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
    public Transform player;
    public NavMeshAgent agent;
    public Light spotlight;
    public GuardBehaviourTree guard;

    [Header("Smoke")]
    // Part of additional smoke implementation. Currently not working
    // Transform and LayerMask required as part of Check Smoke(Class not included in tree currently) and Check Enemy Zone respectively
    //public Transform smoke;
    //public LayerMask smokeMask;

    //public bool smokeVisible = false;
    //public bool smokeSeen = false;

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
            /*
            // Classes for checking if the guard can see smoke and reacting accordingly
            // Currently not working
            
            new Sequence (new List<Node>
            { 
                new CheckSmoke(transform, agent, obstacleMask, guard),
                new GuardSmokeResponse(guard),
            }),

            */

            new Sequence (new List<Node>
            {
                new CheckAttack(guard),
                new GroupAttack(player, guard, agent),
            }),

            new Sequence (new List<Node>
            { 
                new CheckEnemyZone(transform, player, obstacleMask, guard),

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
