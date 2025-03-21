using Core.Player;
using Core.ScriptableObjects;
using Core.Units;
using UnityEngine;
using UnityEngine.AI;

namespace Core.StateMachines.EnemyStates
{
    public class EnemyChaseState : EnemyBaseState
    {
        private readonly PlayerController _player;
        private readonly UnitConfig _config;
        public EnemyChaseState(EnemyController enemyController, 
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
            
        }
        
        public override void Update()
        {
            NavMeshAgent.SetDestination(_player.transform.position);
        }

        public bool HasReachedDestination()
        {
            return Vector3.Distance(Enemy.transform.position, _player.transform.position) <= _config.AttackRange;
        }
    }
}