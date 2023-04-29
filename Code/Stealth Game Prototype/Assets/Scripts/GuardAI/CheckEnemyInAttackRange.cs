using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    private Transform _transform;
    private Transform _player;
    private LayerMask _viewMask;
    private GuardBehaviourTree.ZoneState _zone;


    float angleA = 30;
    float angleC = 150;

    float mediumViewingDist = 25.0f;
    float nearViewingDist = 15.0f;

    public CheckEnemyInAttackRange(Transform transform, Transform player, LayerMask viewMask, GuardBehaviourTree.ZoneState zone)
    {
        _transform = transform;
        _player = player;
        _viewMask = viewMask;
        _zone = zone;
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
                    _zone = GuardBehaviourTree.ZoneState.zone1;
                    state = NodeState.SUCCESS;
                    return state;
                }

                else if (playerGuardAngle < angleC / 2f)
                {
                    _zone = GuardBehaviourTree.ZoneState.zone2;
                    state = NodeState.SUCCESS;
                    return state;
                }

                else
                {
                    _zone = GuardBehaviourTree.ZoneState.emptyZone;
                    state = NodeState.FAILURE;
                    return state;
                }
            }

            // Checks to see the distance between the guard and player
            else if (Vector3.Distance(_transform.position, _player.position) < mediumViewingDist)  //[1]
            {
                // Checks to see the angle between the guard and player
                if (playerGuardAngle < angleA / 2f)
                {
                    _zone = GuardBehaviourTree.ZoneState.zone2;
                    state = NodeState.SUCCESS;
                    return state;
                }

                else
                {
                    _zone = GuardBehaviourTree.ZoneState.emptyZone;
                    state = NodeState.FAILURE;
                    return state;
                }
            }

            else
            {
                _zone = GuardBehaviourTree.ZoneState.emptyZone;
                state = NodeState.FAILURE;
                return state;
            }
        }

        else
        {
            _zone = GuardBehaviourTree.ZoneState.emptyZone;
            state = NodeState.FAILURE;
            return state;
        }
    }
}
