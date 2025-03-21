using System;
using UnityEngine;
using UnityEngine.AI;
using Core.Player;
using Core.ScriptableObjects;
using Core.Services;
using Core.StateMachines;
using Core.StateMachines.EnemyStates;
using Core.Units.AttackBehaviours;
using Lean.Pool;
using UnityEngine.Events;

namespace Core.Units
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : Unit
    {
        public override UnitTeam Team => UnitTeam.Enemy;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject deathFX;
        private UnitConfig _config;
        private NavMeshAgent _navMeshAgent;
        private UnitAttackBehaviour _unitAttack;
        private StateMachine _stateMachine;
        private EnemySpawnState _spawnState;
        private bool _isInitialized;

        public void OnEnable()
        {
            if (!_isInitialized) return;
            
            Health = _config.Health;
            _stateMachine.SetState(_spawnState);
        }

        public void Construct(UnitConfig config, PlayerController player, DamageDealer damageDealer)
        {
            if (_isInitialized) return;
            
            _config = config;
            MaxHealth = config.Health;
            Health = MaxHealth;
            
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _unitAttack = GetComponent<UnitAttackBehaviour>();
            _unitAttack.Initialize(config, damageDealer, player);
            DeathEvent.AddListener(OnDead);
            
            _stateMachine = new StateMachine();

            _spawnState = new EnemySpawnState(this, animator, _navMeshAgent);
            var chaseState = new EnemyChaseState(this, animator, _navMeshAgent, player, config);
            var attackState = new EnemyAttackState(this, animator, _navMeshAgent, player, config);
            var deadState = new EnemyDeadState(this, animator, _navMeshAgent);
            var idleState = new EnemyIdleState(this, animator, _navMeshAgent);
            
            At(_spawnState, chaseState, new FuncPredicate(() => _spawnState.HasReachedDestination()));
            At(chaseState, attackState, new FuncPredicate(() => chaseState.HasReachedDestination()));
            At(attackState, chaseState, new FuncPredicate(() => !attackState.InAttackRange()));
            Any(deadState, new FuncPredicate(() => Health <= 0));
            Any(idleState, new FuncPredicate(() => !player));
            
            _stateMachine.SetState(_spawnState);
        }
        
        private void Update()
        {
            _stateMachine.Update();
        }

        private void At(IState from, IState to, IPredicate condition) =>
            _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) =>
            _stateMachine.AddAnyTransition(to, condition);
        
        public override void Kill(bool withFX = true)
        {
            base.Kill(withFX);
            if (withFX && deathFX) 
                LeanPool.Spawn(deathFX);
        }

        private void OnDead()
        {
            LeanPool.Despawn(gameObject);
        }

        public override void ApplyKnockback(Vector3 knockbackVector)
        {
            _navMeshAgent.velocity = knockbackVector;
        }
    }
}