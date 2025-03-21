using Core.Player;
using Core.ScriptableObjects;
using Core.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Core.StateMachines.EnemyStates
{
    public class EnemyAttackState : EnemyBaseState
    {
        private static readonly int attack = Animator.StringToHash("Attack");
        private readonly PlayerController _player;
        private readonly UnitConfig _config;
        public EnemyAttackState(EnemyController enemyController,
                              Animator animator, 
                              NavMeshAgent navMeshAgent,
                              PlayerController player,
                              UnitConfig config) : base(enemyController, animator, navMeshAgent)
        {
            _player = player;
            _config = config;
        }

        public override void OnEnter()
        {
            NavMeshAgent.destination = Enemy.transform.position;
            Animator.SetBool(attack, true);
        }
        public override void OnExit()
        {
            Animator.SetBool(attack, false);
        }

        public bool InAttackRange()
        {
            return Vector3.Distance(Enemy.transform.position, _player.transform.position) <= _config.AttackRange;
        }
    }
}