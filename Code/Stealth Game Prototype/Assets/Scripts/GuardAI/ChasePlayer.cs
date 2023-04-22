using UnityEngine;
using BehaviorTree;
using UnityEngine.Experimental.GlobalIllumination;

public class ChasePlayer : Node
{
    private Transform _transform;

    public float timePlayerVisible;

    float zone1Timer = 1.0f;
    float zone2Timer = 1.5f;
    float zone3Timer = 2.0f;
    float zone4Timer = 3.0f;
    float zone5Timer = 5.0f;

    Color spottedColour;
    Color initialSpotlightColour;

    public ChasePlayer(Transform transform)
    { 
        _transform = transform;
        initialSpotlightColour = GuardBehaviourTree.spotlight.color; //[2]
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        // Method to chase player if they are caught in a vision zone for enough time

        // If player is within a zone, start a timer
        // If timer exceeds limit, change spotlight colour to indicate detection, chase player and set bools to true

        if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone1)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;  //[2] 
            if (timePlayerVisible >= zone1Timer)
            {
                spottedColour = Color.red;
                GuardBehaviourTree.agent.SetDestination(GuardBehaviourTree.player.position);
                GuardBehaviourTree.attackPlayer = true;
                GuardBehaviourTree.playerSeen = true;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone2)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;  //[2]
            if (timePlayerVisible >= zone2Timer)
            {
                spottedColour = Color.magenta;
                GuardBehaviourTree.agent.SetDestination(GuardBehaviourTree.player.position);
                GuardBehaviourTree.attackPlayer = true;
                GuardBehaviourTree.playerSeen = true;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone3)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone3Timer)
            {
                spottedColour = Color.yellow;
                GuardBehaviourTree.agent.SetDestination(GuardBehaviourTree.player.position);
                GuardBehaviourTree.attackPlayer = true;
                GuardBehaviourTree.playerSeen = true;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone4)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone4Timer)
            {
                spottedColour = Color.green;
                GuardBehaviourTree.agent.SetDestination(GuardBehaviourTree.player.position);
                GuardBehaviourTree.attackPlayer = true;
                GuardBehaviourTree.playerSeen = true;
            }
        }

        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.zone5)
        {
            timePlayerVisible = timePlayerVisible + Time.deltaTime;   //[2]
            if (timePlayerVisible >= zone5Timer)
            {
                spottedColour = Color.blue;
                GuardBehaviourTree.agent.SetDestination(GuardBehaviourTree.player.position);
                GuardBehaviourTree.attackPlayer = true;
                GuardBehaviourTree.playerSeen = true;
            }
        }

        // if player is not in a zone, decremment the timer
        else if (GuardBehaviourTree.zone == GuardBehaviourTree.ZoneState.emptyZone)
        {
            GuardBehaviourTree.attackPlayer = false;
            spottedColour = initialSpotlightColour;
            timePlayerVisible = timePlayerVisible - Time.deltaTime;    //[2]
        }

        // timer doesnt exceed 0 or zone 5 timer
        timePlayerVisible = Mathf.Clamp(timePlayerVisible, 0, zone5Timer);   //[2]
        GuardBehaviourTree.spotlight.color = spottedColour;
    }

}
