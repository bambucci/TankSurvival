using Core.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Core.StateMachines.EnemyStates
{
    public class EnemyVictoryDanceState: EnemyBaseState
    {
        private static readonly int idle = Animator.StringToHash("Idle");
        private static readonly int dance = Animator.StringToHash("Dance");
        private static readonly int danceSpeedMult = Animator.StringToHash("DanceSpeedMult");

        public EnemyVictoryDanceState(EnemyController enemyController, 
            Animator animator, 
            NavMeshAgent navMeshAgent) : base(enemyController, animator, navMeshAgent)
        {
            
        }

        public override void OnEnter()
        {
            Animator.SetFloat(danceSpeedMult, Random.Range(0.8f, 1.2f));
            Animator.SetBool(idle, false);
            Animator.SetTrigger(dance);
            NavMeshAgent.isStopped = true;
        }
    }
}