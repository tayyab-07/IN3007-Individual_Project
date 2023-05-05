using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
    private GuardBehaviourTree _guard;

    public CheckEnemyInAttackRange(GuardBehaviourTree guard)
    {
        _guard = guard;
    }

    public override NodeState Evaluate()
    {
        // return success if the guard is close enough to the player to be within attack range

        if (_guard.attackPlayer == true)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        else
        {
            state = NodeState.FAILURE;
            return state;
        }
    }
}
