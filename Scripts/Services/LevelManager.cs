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
        private readonly WaveSpawnManager _waveSpawnManager;
        private readonly CameraManager _cameraManager;
        private Level _currentLevel;
        
        public LevelManager(GameplayData gameplayData,
                            GameConfig gameConfig,
                            WeaponFactory weaponFactory,
                            StatHandler statHandler,
                            WaveSpawnManager waveSpawnManager,
                            CameraManager cameraManager)
        {
            _gameplayData = gameplayData;
            _gameConfig = gameConfig;
            _weaponFactory = weaponFactory;
            _statHandler = statHandler;
            _waveSpawnManager = waveSpawnManager;
            _cameraManager = cameraManager;
        }

        public void LevelStart()
        {
            _currentLevel ??= Object.Instantiate(_gameConfig.LevelPrefab).GetComponent<Level>();
            var playerController = Object.Instantiate(_gameConfig.PlayerConfig.Prefab,
                    _currentLevel.PlayerSpawnPoint.position,
                    Quaternion.identity)
                .GetComponent<PlayerController>();

            playerController.Construct(_gameConfig.PlayerConfig, _weaponFactory, _statHandler);
            _gameplayData.Initialize(playerController, _currentLevel.PlayerSpawnPoint);
            _waveSpawnManager.StartSpawning();
            _cameraManager.AssignPlayer();
        }

        public void LevelEnd()
        {
            _waveSpawnManager.InterruptSpawning();
        }
    }
}