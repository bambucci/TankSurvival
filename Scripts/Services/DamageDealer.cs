using Core.Data;
using Core.Managers;
using Core.ScriptableObjects;
using Core.Units;
using UnityEngine;
using VContainer;

namespace Core.Services
{
    public class DamageDealer
    {
        private readonly Collider[] _results = new Collider[100];
        private readonly GameConfig _gameConfig;
        private readonly PopUpTextManager _popUpTextManager;

        public DamageDealer(GameConfig gameConfig, PopUpTextManager popUpTextManager)
        {
            _gameConfig = gameConfig;
            _popUpTextManager = popUpTextManager;
        }
        
        public void DealAreaDamage(float damage, Vector3 pos, float radius, float knockbackForce = 0)
        {
            var unitsAffectedCount = Physics.OverlapSphereNonAlloc(pos, radius, _results, _gameConfig.UnitLayer);

            Vector3 knockbackVector = Vector3.zero;
            for (var i = 0; i < unitsAffectedCount; i++)
            {
                if (!_results[i]) continue;

                if (!_results[i].TryGetComponent(out IUnit unit)) continue;

                if (knockbackForce > 0)
                    knockbackVector = (unit.Position - pos).normalized * knockbackForce;
                    
                DealDamage(unit, damage, knockbackVector);
            }
        }

        public void DealDamage(IUnit unit, float damage, Vector3 knockbackVector)
        {
            unit.TakeDamage(damage);
            if (unit.Team == UnitTeam.Enemy)
                _popUpTextManager.Spawn(unit.Position, $"{damage}");

            if (knockbackVector.magnitude > 0)
                unit.ApplyKnockback(knockbackVector);
        }
    }
}