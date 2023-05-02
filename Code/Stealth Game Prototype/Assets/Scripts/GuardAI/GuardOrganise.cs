using BehaviorTree;

public class GuardOrganise : Node
{
    public GuardOrganise() 
    { 

    }

    public override NodeState Evaluate()
    {

        if (GuardBehaviourTree.playerVisible == true)
        {
            GuardBehaviourTree.conductAttack = true;
            state = NodeState.SUCCESS; 
            return state;
        }

        else if (GuardBehaviourTree.playerVisible == false && GuardBehaviourTree.playerSeen == true)
        {
            GuardBehaviourTree.conductAttack = true;
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