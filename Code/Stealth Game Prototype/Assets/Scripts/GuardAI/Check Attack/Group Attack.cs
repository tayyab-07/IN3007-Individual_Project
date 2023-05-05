using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class GroupAttack : Node
{
    private Transform _player;
    private GuardBehaviourTree _guard;
    private NavMeshAgent _agent;

    public GroupAttack(Transform player, GuardBehaviourTree guard, NavMeshAgent agent)
    {
        _player = player;
        _guard = guard;
        _agent = agent;
    }

    public override NodeState Evaluate()
    {
        // Sets the guard to go to the player position
        // return failure to allow for the rest of the tree to run
        // doing it this way allows the guard to still travell to the player but also use the following branch to check their zone

        _agent.SetDestination(_player.position);
        state = NodeState.FAILURE; 
        return state;
    }
}