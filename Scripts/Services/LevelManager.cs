using Core.Data;
using Core.Managers;
using Core.Player;
using Core.ScriptableObjects;
using Core.Stats;
using UnityEngine;

namespace Core.Services
{
    public class LevelManager
    {
        private readonly GameplayData _gameplayData;
        private readonly GameConfig _gameConfig;
        private readonly WeaponFactory _weaponFactory;
        private readonly StatHandler _statHandler;
        
        public LevelManager(GameplayData gameplayData,
                            GameConfig gameConfig,
                            WeaponFactory weaponFactory,
                            StatHandler statHandler)
        {
            _gameplayData = gameplayData;
            _gameConfig = gameConfig;
            _weaponFactory = weaponFactory;
            _statHandler = statHandler;
        }

        public void Initialize()
        {
            var level = Object.Instantiate(_gameConfig.LevelPrefab).GetComponent<Level>();
            var playerController = Object.Instantiate(_gameConfig.PlayerConfig.Prefab, 
                                                                level.PlayerSpawnPoint.position, 
                                                                Quaternion.identity)
                                                                .GetComponent<PlayerController>();
            
            playerController.Construct(_gameConfig.PlayerConfig, _weaponFactory, _statHandler);
            _gameplayData.Initialize(playerController, level.PlayerSpawnPoint);
        }
    }
}