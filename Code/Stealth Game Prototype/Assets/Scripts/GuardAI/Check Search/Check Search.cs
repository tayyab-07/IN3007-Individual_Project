using BehaviorTree;

public class CheckSearch : Node
{
    private GuardBehaviourTree _guard;

    public CheckSearch(GuardBehaviourTree guard)
    {
        _guard = guard;
    }

    public override NodeState Evaluate()
    {
        // return success if the group has organised a search

        if (_guard.organiseSearch == true)
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
