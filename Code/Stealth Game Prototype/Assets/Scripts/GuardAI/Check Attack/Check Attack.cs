using BehaviorTree;
public class CheckAttack : Node
{
    private GuardBehaviourTree _guard;

    public CheckAttack(GuardBehaviourTree guard) 
    { 
        _guard = guard;
    }

    public override NodeState Evaluate()
    {
        // Returns success if group are attacking
        // feeds into Group Attack.cs

        if (_guard.organiseAttack == true)
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
