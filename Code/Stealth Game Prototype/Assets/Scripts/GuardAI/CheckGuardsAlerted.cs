using BehaviorTree;

public class CheckGuardsAlerted : Node
{
    private bool _playerSeen;

    public CheckGuardsAlerted(bool playerSeen) 
    { 
        _playerSeen = playerSeen;
    }

    public override NodeState Evaluate()
    {
        if (GuardBehaviourTree.playerVisible == true)
        {
            state = NodeState.SUCCESS;
            return state;
        }

        else if (GuardBehaviourTree.playerVisible == false && _playerSeen == true)
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
