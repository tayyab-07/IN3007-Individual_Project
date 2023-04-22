using BehaviorTree;
using UnityEngine;
using static Guard;

public class CheckEnemyVisible : Node
{
    private Transform _transform;

    float angleA = 30;
    float angleB = 90;
    float angleC = 150;

    float farViewingDist = 40.0f;
    float mediumViewingDist = 25.0f;
    float nearViewingDist = 15.0f;


    public CheckEnemyVisible(Transform transform)
    { 
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");

        if (t == null) 
        {
            // conditional statements to find out what zone a player is in
            // always set to empty zone if any condition isnt met

            Vector3 distToPlayer = (GuardBehaviourTree.player.position - _transform.position).normalized;  //[1]
            float playerGuardAngle = Vector3.Angle(_transform.forward, distToPlayer);  //[1]

            // Checks if there is an obstacle between the guard and player
            if (!Physics.Linecast(_transform.position, GuardBehaviourTree.player.position, GuardBehaviourTree.viewMask))
            {
                // Checks to see the distance between the guard and player
                if (Vector3.Distance(_transform.position, GuardBehaviourTree.player.position) < nearViewingDist)  //[1]
                {
                    // Checks to see the angle between the guard and player
                    if (playerGuardAngle < angleA / 2f)
                    {
                        GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.zone1;
                        //parent.parent.SetData("target", GuardBehaviourTree.player);
                        state = NodeState.SUCCESS;
                        return state;
                    }

                    else if (playerGuardAngle < angleC / 2f)
                    {
                        GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.zone2;
                        //parent.parent.SetData("target", GuardBehaviourTree.player);
                        state = NodeState.SUCCESS;
                        return state;
                    }

                    else
                    {
                        GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.emptyZone;
                        state = NodeState.FAILURE;
                        return state;
                    }
                }

                // Checks to see the distance between the guard and player
                else if (Vector3.Distance(_transform.position, GuardBehaviourTree.player.position) < mediumViewingDist)  //[1]
                {
                    // Checks to see the angle between the guard and player
                    if (playerGuardAngle < angleA / 2f)
                    {
                        GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.zone2;
                        //parent.parent.SetData("target", GuardBehaviourTree.player);
                        state = NodeState.SUCCESS;
                        return state;
                    }

                    else if (playerGuardAngle < angleB / 2f)
                    {
                        GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.zone3;
                        //parent.parent.SetData("target", GuardBehaviourTree.player);
                        state = NodeState.SUCCESS;
                        return state;
                    }

                    else if (playerGuardAngle < angleC / 2f)
                    {
                        GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.zone4;
                        //parent.parent.SetData("target", GuardBehaviourTree.player);
                        state = NodeState.SUCCESS;
                        return state;
                    }

                    else
                    {
                        GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.emptyZone;
                        state = NodeState.FAILURE;
                        return state;
                    }
                }

                // Checks to see the distance between the guard and player
                else if (Vector3.Distance(_transform.position, GuardBehaviourTree.player.position) < farViewingDist)  //[1]
                {
                    // Checks to see the angle between the guard and player
                    if (playerGuardAngle < angleA / 2f)
                    {
                        GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.zone3;
                        //parent.parent.SetData("target", GuardBehaviourTree.player);
                        state = NodeState.SUCCESS;
                        return state;
                    }

                    else if (playerGuardAngle < angleB / 2f)
                    {
                        GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.zone4;
                        //parent.parent.SetData("target", GuardBehaviourTree.player);
                        state = NodeState.SUCCESS;
                        return state;
                    }

                    else if (playerGuardAngle < angleC / 2f)
                    {
                        GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.zone5;
                        //parent.parent.SetData("target", GuardBehaviourTree.player);
                        state = NodeState.SUCCESS;
                        return state;
                    }

                    else
                    {
                        GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.emptyZone;
                        state = NodeState.FAILURE;
                        return state;
                    }
                }

                else
                {
                    GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.emptyZone;
                    state = NodeState.FAILURE;
                    return state;
                }
            }

            else
            {
                GuardBehaviourTree.zone = GuardBehaviourTree.ZoneState.emptyZone;
                state = NodeState.FAILURE;
                return state;
            }
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
