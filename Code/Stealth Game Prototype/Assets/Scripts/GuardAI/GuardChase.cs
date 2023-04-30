using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class GuardChase : Node
{
    private Transform _player;
    private NavMeshAgent _agent;
    private Light _spotlight;

    float zone1Timer = 1.0f;
    float zone2Timer = 1.5f;
    float zone3Timer = 2.0f;
    float zone4Timer = 3.0f;
    float zone5Timer = 5.0f;

    Color spottedColour;
    Color initialSpotlightColour;

    public GuardChase(Transform player, NavMeshAgent agent, Light spotlight)
    { 
        _player = player;
        _agent = agent;
        _spotlight = spotlight;
    }

    public override NodeState Evaluate()
    {
        initialSpotlightColour = _spotlight.color; //[2]

        // timer doesnt exceed 0 or zone 5 timer
        GuardBehaviourTree.timePlayerVisible = Mathf.Clamp(GuardBehaviourTree.timePlayerVisible, 0, zone5Timer);   //[2]

        // Method to chase player if they are caught in a vision zone for enough time

        // If player is within a zone, start a timer
        // If timer exceeds limit, change spotlight colour to indicate detection, chase player and set bools to true

        if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone1)
        {
            GuardBehaviourTree.timePlayerVisible = GuardBehaviourTree.timePlayerVisible + Time.deltaTime;  //[2] 
            if (GuardBehaviourTree.timePlayerVisible >= zone1Timer)
            {
                spottedColour = Color.red;
                Chaseplayer();
                state = NodeState.SUCCESS;
                return state;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone2)
        {
            GuardBehaviourTree.timePlayerVisible = GuardBehaviourTree.timePlayerVisible + Time.deltaTime;  //[2]
            if (GuardBehaviourTree.timePlayerVisible >= zone2Timer)
            {
                spottedColour = Color.magenta;
                Chaseplayer();
                state = NodeState.SUCCESS;
                return state;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone3)
        {
            GuardBehaviourTree.timePlayerVisible = GuardBehaviourTree.timePlayerVisible + Time.deltaTime;   //[2]
            if (GuardBehaviourTree.timePlayerVisible >= zone3Timer)
            {
                spottedColour = Color.yellow;
                Chaseplayer();
                state = NodeState.SUCCESS;
                return state;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone4)
        {
            GuardBehaviourTree.timePlayerVisible = GuardBehaviourTree.timePlayerVisible + Time.deltaTime;   //[2]
            if (GuardBehaviourTree.timePlayerVisible >= zone4Timer)
            {
                spottedColour = Color.green;
                Chaseplayer();
                state = NodeState.SUCCESS;
                return state;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone5)
        {
            GuardBehaviourTree.timePlayerVisible = GuardBehaviourTree.timePlayerVisible + Time.deltaTime;   //[2]
            if (GuardBehaviourTree.timePlayerVisible >= zone5Timer)
            {
                spottedColour = Color.blue;
                Chaseplayer();
                state = NodeState.SUCCESS;
                return state;
            }
        }

        // if player is not in a zone, decrement the timer
        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.emptyZone)
        {
            GuardBehaviourTree.attackPlayer = false;
            spottedColour = initialSpotlightColour;
            GuardBehaviourTree.timePlayerVisible = GuardBehaviourTree.timePlayerVisible - Time.deltaTime;    //[2]

            if (GuardBehaviourTree.timePlayerVisible == 0)
            {
                state = NodeState.FAILURE;
                return state;
            }

            else
            {
                state = NodeState.RUNNING;
                return state;
            }
        }

        
        _spotlight.color = spottedColour;

        state = NodeState.FAILURE;
        return state;

    }

    public void Chaseplayer()
    {
        _agent.SetDestination(_player.position);
        GuardBehaviourTree.attackPlayer = true;
        GuardBehaviourTree.playerSeen = true;
    }

}
