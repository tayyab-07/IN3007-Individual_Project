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
