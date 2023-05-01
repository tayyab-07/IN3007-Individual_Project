using BehaviorTree;
using UnityEngine.AI;
using UnityEngine;

public class CheckEnemySpotted : Node
{
    private Light _spotlight;

    float zone1Timer = 1.0f;
    float zone2Timer = 1.5f;
    float zone3Timer = 2.0f;
    float zone4Timer = 3.0f;
    float zone5Timer = 5.0f;

    Color initialSpotlightColour;

    public CheckEnemySpotted(Light spotlight)
    {
        _spotlight = spotlight;
        initialSpotlightColour = _spotlight.color; //[2]
    }

    public override NodeState Evaluate()
    {
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
                _spotlight.color = Color.red;
                state = NodeState.SUCCESS;
                return state;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone2)
        {
            GuardBehaviourTree.timePlayerVisible = GuardBehaviourTree.timePlayerVisible + Time.deltaTime;  //[2]
            if (GuardBehaviourTree.timePlayerVisible >= zone2Timer)
            {
                _spotlight.color = Color.magenta;
                state = NodeState.SUCCESS;
                return state;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone3)
        {
            GuardBehaviourTree.timePlayerVisible = GuardBehaviourTree.timePlayerVisible + Time.deltaTime;   //[2]
            if (GuardBehaviourTree.timePlayerVisible >= zone3Timer)
            {
                _spotlight.color = Color.yellow;
                state = NodeState.SUCCESS;
                return state;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone4)
        {
            GuardBehaviourTree.timePlayerVisible = GuardBehaviourTree.timePlayerVisible + Time.deltaTime;   //[2]
            if (GuardBehaviourTree.timePlayerVisible >= zone4Timer)
            {
                _spotlight.color = Color.green;
                state = NodeState.SUCCESS;
                return state;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone5)
        {
            GuardBehaviourTree.timePlayerVisible = GuardBehaviourTree.timePlayerVisible + Time.deltaTime;   //[2]
            if (GuardBehaviourTree.timePlayerVisible >= zone5Timer)
            {
                _spotlight.color = Color.blue;
                state = NodeState.SUCCESS;
                return state;
            }
        }

        // if player is not in a zone, decrement the timer
        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.emptyZone)
        {
            _spotlight.color = initialSpotlightColour;
            GuardBehaviourTree.timePlayerVisible = GuardBehaviourTree.timePlayerVisible - Time.deltaTime;    //[2]

            if (GuardBehaviourTree.timePlayerVisible <= 0)
            {
                state = NodeState.FAILURE;
                return state;
            }

            state = NodeState.RUNNING; 
            return state;
        }

        state = NodeState.RUNNING;
        return state;

    }



}
