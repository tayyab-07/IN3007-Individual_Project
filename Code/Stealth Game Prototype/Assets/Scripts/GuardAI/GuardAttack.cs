using BehaviorTree;
using UnityEngine;

public class GuardAttack : Node
{
    // IMPLEMENT ATTACK CLASS 

    private Transform _transform;

    public GuardAttack(Transform transform) 
    { 
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        state = NodeState.RUNNING;
        return state;
    }

}
