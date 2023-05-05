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
        // placeholder class for now, sets the spotlight color to green when the guard should be attacking
        // soon i will arm each guard with a gun and make them shoot at the player here

        _spotlight.color = Color.green;
        state = NodeState.SUCCESS;
        return state;
    }

}
