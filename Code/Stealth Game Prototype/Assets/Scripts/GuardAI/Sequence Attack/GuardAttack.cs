using BehaviorTree;
using UnityEngine;

public class GuardAttack : Node
{
    private Light _spotlight;

    public GuardAttack(Light spotlight) 
    { 
        _spotlight = spotlight;
    }

    public override NodeState Evaluate()
    {
        _spotlight.color = Color.green;
        state = NodeState.SUCCESS;
        return state;
    }

}
