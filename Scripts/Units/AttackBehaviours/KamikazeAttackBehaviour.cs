using Core.Data;
using Core.Player;
using Core.ScriptableObjects;
using Core.Services;
using Lean.Pool;
using UnityEngine;

namespace Core.Units.AttackBehaviours
{
    public class KamikazeAttackBehaviour : UnitAttackBehaviour
    {
        [SerializeField] private GameObject explosionFX;
        private UnitConfig _unitConfig;
        private DamageDealer _damageDealer;
        private PlayerController _player;
        public override void Initialize(UnitConfig unitConfig , 
                                        DamageDealer damageDealer, 
                                        PlayerController player)
        {
            _unitConfig = unitConfig;
            _damageDealer = damageDealer;
            _player = player;
        }
        
        public override void Attack()
        {
            if (_player == null) return;

            var knockbackVector = Vector3.zero;
            if (_unitConfig.KnockbackForce > 0)
                knockbackVector = (_player.Position - transform.position).normalized * _unitConfig.KnockbackForce;
            _damageDealer.DealDamage(_player, _unitConfig.AttackDamage, knockbackVector);
            
            if(explosionFX != null) 
                LeanPool.Spawn(explosionFX, transform.position, explosionFX.transform.rotation);
            
            GetComponent<EnemyController>().Kill(false);
        }
    }
}