using BehaviorTree;

public class GuardOrganise : Node
{
    private GuardBehaviourTree _guard;

    public GuardOrganise(GuardBehaviourTree guard) 
    {
        _guard = guard;
    }

    public override NodeState Evaluate()
    {

        if (_guard.attackPlayer == true)
        {
            _guard.conductAttack = true;
            state = NodeState.SUCCESS; 
            return state;
        }

        else if (_guard.zone == GuardBehaviourTree.ZoneState.emptyZone && _guard.playerSeen == true)
        {
            _guard.conductSearch = true;
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