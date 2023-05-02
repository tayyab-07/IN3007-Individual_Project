using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{

    public CheckEnemyInAttackRange()
    {
        
    }

    public override NodeState Evaluate()
    {
        if (GuardBehaviourTree.attackPlayer == true)
        {
            GuardBehaviourTree.attackPlayer = false;
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
