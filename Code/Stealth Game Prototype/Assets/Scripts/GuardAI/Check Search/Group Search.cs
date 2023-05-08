using BehaviorTree;

public class GroupSearch : Node
{
    private AlertedSprite _alertedSprite;
    private SearchingSprite _searchingSprite;

    public GroupSearch(AlertedSprite alertedSprite, SearchingSprite searchingSprite)
    {
        _alertedSprite = alertedSprite;
        _searchingSprite = searchingSprite;
    }

    public override NodeState Evaluate()
    {
        // very basic class to stop the guard from doing anything if they are searching
        // searching actions are determined in BTGuardGroup
        // in order to do this the guard cant be chasing or patrolling etc...
        // therefore this simple class is used to ensure the guard is not doing anything else while searching

        _alertedSprite.disableAlerted();
        _searchingSprite.displaySearching();
        state = NodeState.SUCCESS; 
        return state;
    }
}
