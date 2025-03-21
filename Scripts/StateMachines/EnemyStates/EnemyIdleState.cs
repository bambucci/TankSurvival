using Core.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Core.StateMachines.EnemyStates
{
    public class EnemyIdleState: EnemyBaseState
    {
        private static readonly int idle = Animator.StringToHash("Idle");

        public EnemyIdleState(EnemyController enemyController, 
            Animator animator, 
            NavMeshAgent navMeshAgent) : base(enemyController, animator, navMeshAgent)
        {
            
        }

        public override void OnEnter()
        {
            Animator.SetBool(idle, true);
            NavMeshAgent.isStopped = true;
        }
    }
}