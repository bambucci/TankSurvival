using Core.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Core.StateMachines.EnemyStates
{
    public class EnemyDeadState : EnemyBaseState
    {
        public EnemyDeadState(EnemyController enemyController,
            Animator animator,
            NavMeshAgent navMeshAgent) : base(enemyController, animator, navMeshAgent)
        {
        }

        public override void OnEnter()
        {
            
        }
    }
}