using System.Collections.Generic;
using Core.Player;
using Core.Units;
using UnityEngine;

namespace Core.Data
{
    public class GameplayData
    {
        public PlayerController PlayerController { get; private set; }
        public Transform PlayerSpawnPoint { get; private set; }
        public List<EnemyController> EnemiesAlive { get; private set; }

        public void Initialize(PlayerController playerController,
                               Transform playerSpawnPoint)
        {
            PlayerController = playerController;
            PlayerSpawnPoint = playerSpawnPoint;
            EnemiesAlive = new List<EnemyController>();
        }
        
        public void EnemyDied(EnemyController enemy) => EnemiesAlive.Remove(enemy);
    }
}