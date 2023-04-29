using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;

public class GuardChase : Node
{
    private Transform _player;
    private NavMeshAgent _agent;
    private Light _spotlight;
    private GuardBehaviourTree.ZoneState _zone;

    public float timePlayerVisible;

    float zone1Timer = 1.0f;
    float zone2Timer = 1.5f;
    float zone3Timer = 2.0f;
    float zone4Timer = 3.0f;
    float zone5Timer = 5.0f;

    Color spottedColour;
    Color initialSpotlightColour;

    public GuardChase(Transform player, NavMeshAgent agent, Light spotlight, GuardBehaviourTree.ZoneState zone)
    { 
        _player = player;
        _agent = agent;
        _spotlight = spotlight;
        _zone = zone;
    }

    public override NodeState Evaluate()
    {
        initialSpotlightColour = _spotlight.color; //[2]

        // Method to chase player if they are caught in a vision zone for enough time

        // If player is within a zone, start a timer
        // If timer exceeds limit, change spotlight colour to indicate detection, chase player and set bools to true

        if (_zone == GuardBehaviourTree.ZoneState.zone1)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;  //[2] 
            if (timePlayerVisible >= zone1Timer)
            {
                spottedColour = Color.red;
                Chaseplayer();
            }
        }

        else if (_zone == GuardBehaviourTree.ZoneState.zone2)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;  //[2]
            if (timePlayerVisible >= zone2Timer)
            {
                spottedColour = Color.magenta;
                Chaseplayer();
            }
        }

        else if (_zone == GuardBehaviourTree.ZoneState.zone3)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone3Timer)
            {
                spottedColour = Color.yellow;
                Chaseplayer();
            }
        }

        else if (_zone == GuardBehaviourTree.ZoneState.zone4)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone4Timer)
            {
                spottedColour = Color.green;
                Chaseplayer();
            }
        }

        else if (_zone == GuardBehaviourTree.ZoneState.zone5)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone5Timer)
            {
                spottedColour = Color.blue;
                Chaseplayer();
            }
        }

        // if player is not in a zone, decrement the timer
        else if (_zone == GuardBehaviourTree.ZoneState.emptyZone)
        {
            GuardBehaviourTree.attackPlayer = false;
            spottedColour = initialSpotlightColour;
            timePlayerVisible = timePlayerVisible - Time.deltaTime;    //[2]

            if (timePlayerVisible == 0)
            {
                state = NodeState.FAILURE;
                return state;
            }
        }

        // timer doesnt exceed 0 or zone 5 timer
        timePlayerVisible = Mathf.Clamp(timePlayerVisible, 0, zone5Timer);   //[2]
        _spotlight.color = spottedColour;

        state = NodeState.RUNNING;
        return state;

    }

    public NodeState Chaseplayer()
    {
        _agent.SetDestination(_player.position);
        GuardBehaviourTree.attackPlayer = true;
        GuardBehaviourTree.playerSeen = true;

        state = NodeState.SUCCESS;
        return state;
    }

}
