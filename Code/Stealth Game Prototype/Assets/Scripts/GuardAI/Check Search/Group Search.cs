using BehaviorTree;

public class GroupSearch : Node
{
    public GroupSearch()
    {

    }

    public override NodeState Evaluate()
    {
        // very basic class to stop the guard from doing anything if they are searching
        // searching actions are determined in BTGuardGroup
        // in order to do this the guard cant be chasing or patrolling etc...
        // therefore this simple class is used to ensure the guard is not doing anything else while searching

        state = NodeState.SUCCESS; 
        return state;
    }
}
