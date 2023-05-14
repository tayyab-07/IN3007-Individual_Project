using BehaviorTree;

public class GuardSmokeResponse : Node
{
    private GuardBehaviourTree _guard;

    public GuardSmokeResponse(GuardBehaviourTree guard) 
    { 
        _guard = guard;
    }

    public override NodeState Evaluate()
    {
        _guard.smokeSeen = true;
        _guard.smokeVisible = true;

        state = NodeState.SUCCESS; 
        return state;
    }

}
