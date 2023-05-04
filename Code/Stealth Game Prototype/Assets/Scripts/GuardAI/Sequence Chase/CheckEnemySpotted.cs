using BehaviorTree;
using UnityEngine;

public class CheckEnemySpotted : Node
{
    private Light _spotlight;
    private GuardBehaviourTree _guard;

    float zone1Timer = 1.0f;
    float zone2Timer = 1.5f;
    float zone3Timer = 2.0f;
    float zone4Timer = 3.0f;
    float zone5Timer = 5.0f;

    Color initialSpotlightColour;

    public CheckEnemySpotted(Light spotlight, GuardBehaviourTree guard)
    {
        _spotlight = spotlight;
        _guard = guard;
    }

    public override NodeState Evaluate()
    {
        initialSpotlightColour = _spotlight.color; //[2]

        // timer doesnt exceed 0 or zone 5 timer
        _guard.timePlayerVisible = Mathf.Clamp(_guard.timePlayerVisible, 0, zone5Timer);   //[2]

        // Method to chase player if they are caught in a vision zone for enough time

        // If player is within a zone, start a timer
        // If timer exceeds limit, change spotlight colour to indicate detection, chase player and set bools to true

        if (_guard.zone == GuardBehaviourTree.ZoneState.zone1)
        {
            _guard.timePlayerVisible = _guard.timePlayerVisible + Time.deltaTime;  //[2] 
            if (_guard.timePlayerVisible >= zone1Timer)
            {
                _spotlight.color = Color.red;
                _guard.attackPlayer = true;
                _guard.playerSeen = true;
            }
        }

        else if (_guard.zone == GuardBehaviourTree.ZoneState.zone2)
        {
            _guard.timePlayerVisible = _guard.timePlayerVisible + Time.deltaTime;  //[2]
            if (_guard.timePlayerVisible >= zone2Timer)
            {
                _spotlight.color = Color.magenta;
                _guard.attackPlayer = false;
                _guard.playerSeen = true;
            }
        }

        else if (_guard.zone == GuardBehaviourTree.ZoneState.zone3)
        {
            _guard.timePlayerVisible = _guard.timePlayerVisible + Time.deltaTime;   //[2]
            if (_guard.timePlayerVisible >= zone3Timer)
            {
                _spotlight.color = Color.yellow;
                _guard.attackPlayer = false;
                _guard.playerSeen = true;
            }
        }

        else if (_guard.zone == GuardBehaviourTree.ZoneState.zone4)
        {
            _guard.timePlayerVisible = _guard.timePlayerVisible + Time.deltaTime;   //[2]
            if (_guard.timePlayerVisible >= zone4Timer)
            {
                _spotlight.color = Color.green;
                _guard.attackPlayer = false;
                _guard.playerSeen = true;
            }
        }

        else if (_guard.zone == GuardBehaviourTree.ZoneState.zone5)
        {
            _guard.timePlayerVisible = _guard.timePlayerVisible + Time.deltaTime;   //[2]
            if (_guard.timePlayerVisible >= zone5Timer)
            {
                _spotlight.color = Color.blue;
                _guard.attackPlayer = false;
                _guard.playerSeen = true;
            }
        }

        // if player is not in a zone, decrement the timer
        else if (_guard.zone == GuardBehaviourTree.ZoneState.emptyZone)
        {
            _spotlight.color = Color.white;
            _guard.attackPlayer = false;
            _guard.timePlayerVisible = _guard.timePlayerVisible - Time.deltaTime;    //[2]

            if (_guard.timePlayerVisible <= 0)
            {
                state = NodeState.FAILURE;
                return state;
            }
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
