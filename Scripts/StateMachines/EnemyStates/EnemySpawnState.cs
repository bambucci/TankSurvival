using Core.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Core.StateMachines.EnemyStates
{
    public class EnemySpawnState : EnemyBaseState
    {
        private static readonly int move = Animator.StringToHash("Move");

        public EnemySpawnState(EnemyController enemyController, 
                               Animator animator, 
                               NavMeshAgent navMeshAgent) : base(enemyController, animator, navMeshAgent)
        {
            
        }

        public override void OnEnter()
        {
            Animator.SetBool(move, true);
            NavMeshAgent.SetDestination( Enemy.transform.position + Enemy.transform.forward * 1);
        }

        public bool HasReachedDestination()
        {
            return !NavMeshAgent.pathPending
                   && NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance * 2;
        }
    }
}