using Core.ScriptableObjects;
using Core.Services;
using Core.Stats;
using Core.Units;
using Lean.Pool;
using UnityEngine;

namespace Core.Player
{
    public class PlayerController: Unit
    {
        [SerializeField] private GameObject deathFX;
        public override UnitTeam Team => UnitTeam.Player;
        private UnitConfig _config;
        private PlayerMovement _playerMovement;
        private PlayerAttack _playerAttack;

        public void Construct(UnitConfig unitConfig,
                              WeaponFactory weaponFactory,
                              StatHandler statHandler)
        {
            _config = unitConfig;
            _playerAttack = GetComponent<PlayerAttack>();
            _playerMovement = GetComponent<PlayerMovement>();
            
            _playerAttack.Initialize(weaponFactory);
            _playerMovement.Initialize(statHandler);
            InitializeHealth(statHandler);
        }

        private void InitializeHealth(StatHandler statHandler)
        {
            var healthStat = statHandler.GetStat(BaseStat.Health);
            MaxHealth = healthStat.BaseValue;
            Health = MaxHealth;
            
            healthStat.OnStatChanged += OnHealthStatChange;
            DeathEvent.AddListener(OnDead);
        }

        private void OnDead()
        {
            if (deathFX != null)
                LeanPool.Spawn(deathFX, transform.position, deathFX.transform.rotation);
            Destroy(gameObject);
        }

        public override void ApplyKnockback(Vector3 knockbackVector)
        {
            _playerMovement.ApplyForce(knockbackVector);
        }

        private void OnHealthStatChange(float newMaxHealth)
        {
            var healthDifference = Mathf.Clamp(newMaxHealth - MaxHealth, 0, Mathf.Infinity);
            MaxHealth = newMaxHealth;
            Heal(healthDifference);
        }
    }
}