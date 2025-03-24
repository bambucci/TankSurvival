using Core.Data;
using Core.ScriptableObjects;
using Core.Units;
using UnityEngine;
using Lean.Pool;
using VContainer;

namespace Core.Services
{
    public class UnitFactory
    {
        private readonly GameplayData _gameplayData;
        private readonly DamageDealer _damageDealer;
        
        public UnitFactory(GameplayData gameplayData, DamageDealer damageDealer)
        {
            _gameplayData = gameplayData;
            _damageDealer = damageDealer;
        }
        
        public EnemyController CreateEnemyUnit(UnitConfig config, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var newUnit = LeanPool.Spawn(config.Prefab, position, rotation, parent);
            var enemyController = newUnit.GetComponent<EnemyController>();
            enemyController.Construct(config, _gameplayData.PlayerController, _damageDealer);
            enemyController.DeathEvent.AddListener(() => _gameplayData.RemoveEnemy(enemyController));
            
            _gameplayData.EnemiesAlive.Add(enemyController);
            return enemyController;
        }
    }
}