using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class GuardChase : Node
{
    private Transform _player;
    private NavMeshAgent _agent;

    public GuardChase(Transform player, NavMeshAgent agent)
    {
        _player = player;
        _agent = agent;
    }

    public override NodeState Evaluate()
    {
        // chases after the player 
        // this class is called if the player is visible but not close enough to be within attack range

        _agent.SetDestination(_player.position);
        state = NodeState.SUCCESS; 
        return state;
    }

}
