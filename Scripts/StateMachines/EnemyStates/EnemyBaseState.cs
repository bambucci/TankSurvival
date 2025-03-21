using Core.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Core.StateMachines.EnemyStates
{
    public abstract class EnemyBaseState : IState
    {
        protected readonly EnemyController Enemy;
        protected readonly Animator Animator;
        protected readonly NavMeshAgent NavMeshAgent;

        protected EnemyBaseState(EnemyController enemyController,
                            Animator animator,
                            NavMeshAgent navMeshAgent)
        {
            Enemy = enemyController;
            Animator = animator;
            NavMeshAgent = navMeshAgent;
        }
        
        public virtual void OnEnter()
        {
            
        }
        public virtual void Update()
        {
            
        }
        public virtual void FixedUpdate()
        {
            
        }
        public virtual void OnExit()
        {
            
        }
    }
}