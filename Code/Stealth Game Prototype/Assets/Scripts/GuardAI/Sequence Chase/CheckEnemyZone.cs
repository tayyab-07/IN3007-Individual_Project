using BehaviorTree;
using UnityEngine;

public class CheckEnemyZone : Node
{
    private Transform _transform;
    private Transform _player;
    private LayerMask _viewMask;
    private GuardBehaviourTree _guard;

    float angleA = 30;
    float angleB = 90;
    float angleC = 150;

    float farViewingDist = 40.0f;
    float mediumViewingDist = 25.0f;
    float nearViewingDist = 15.0f;

    public CheckEnemyZone(Transform transform, Transform player, LayerMask viewMask, GuardBehaviourTree guard)
    {
        _transform = transform;
        _player = player;
        _viewMask = viewMask;
        _guard = guard;
    }

    public override NodeState Evaluate()
    {
        // conditional statements to find out what zone a player is in
        // always set to empty zone if any condition isnt met
        
        Vector3 distToPlayer = (_player.position - _transform.position).normalized;  //[1]
        float playerGuardAngle = Vector3.Angle(_transform.forward, distToPlayer);  //[1]

        // Checks if there is an obstacle between the guard and player
        if (!Physics.Linecast(_transform.position, _player.position, _viewMask))
        {
            // Checks to see the distance between the guard and player
            if (Vector3.Distance(_transform.position, _player.position) < nearViewingDist)  //[1]
            {
                // Checks to see the angle between the guard and player
                if (playerGuardAngle < angleA / 2f)
                {
                    _guard.zone = GuardBehaviourTree.ZoneState.zone1;
                }

                else if (playerGuardAngle < angleC / 2f)
                {
                    _guard.zone = GuardBehaviourTree.ZoneState.zone2;
                }

                else
                {
                    _guard.zone = GuardBehaviourTree.ZoneState.emptyZone;
                }
            }

            // Checks to see the distance between the guard and player
            else if (Vector3.Distance(_transform.position, _player.position) < mediumViewingDist)  //[1]
            {
                // Checks to see the angle between the guard and player
                if (playerGuardAngle < angleA / 2f)
                {
                    _guard.zone = GuardBehaviourTree.ZoneState.zone2;
                }

                else if (playerGuardAngle < angleB / 2f)
                {
                    _guard.zone = GuardBehaviourTree.ZoneState.zone3;
                }

                else if (playerGuardAngle < angleC / 2f)
                {
                    _guard.zone = GuardBehaviourTree.ZoneState.zone4;
                }

                else
                {
                    _guard.zone = GuardBehaviourTree.ZoneState.emptyZone;
                }
            }

            // Checks to see the distance between the guard and player
            else if (Vector3.Distance(_transform.position, _player.position) < farViewingDist)  //[1]
            {
                // Checks to see the angle between the guard and player
                if (playerGuardAngle < angleA / 2f)
                {
                    _guard.zone = GuardBehaviourTree.ZoneState.zone3;
                }

                else if (playerGuardAngle < angleB / 2f)
                {
                    _guard.zone = GuardBehaviourTree.ZoneState.zone4;
                }

                else if (playerGuardAngle < angleC / 2f)
                {
                    _guard.zone = GuardBehaviourTree.ZoneState.zone5;
                }

                else
                {
                    _guard.zone = GuardBehaviourTree.ZoneState.emptyZone;
                }
            }

            else
            {
                _guard.zone = GuardBehaviourTree.ZoneState.emptyZone;
            }
        }

        else
        {
            _guard.zone = GuardBehaviourTree.ZoneState.emptyZone;
        }

        // Return success regardless of the outcome as outcomes can only be zones 1 through 5 and empty zone which are all necesary for the next leaf
        state = NodeState.SUCCESS;
        return state;
    }
}
