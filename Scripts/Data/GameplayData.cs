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

        public int CurrentScore { get; private set; }
        public int HighScore { get; private set; }

        public void Initialize(PlayerController playerController,
                               Transform playerSpawnPoint)
        {
            PlayerController = playerController;
            PlayerSpawnPoint = playerSpawnPoint;
            
            EnemiesAlive ??= new List<EnemyController>();
            EnemiesAlive?.Clear();
        }

        public void Clear()
        {
            PlayerController = null;
            PlayerSpawnPoint = null;
            
            foreach (var enemy in EnemiesAlive)
            {
                UnsubscribeFromEnemyDeath(enemy);
                enemy.Despawn();
            }

            CurrentScore = 0;
        }

        public void RemoveEnemy(EnemyController enemy)
        {
            UnsubscribeFromEnemyDeath(enemy);
            EnemiesAlive.Remove(enemy);

            CalculateScore();
        }

        private void CalculateScore()
        {
            CurrentScore += 5;
            if (CurrentScore > HighScore)
                HighScore = CurrentScore;
        }
        
        
        private void UnsubscribeFromEnemyDeath(EnemyController enemy) => enemy.DeathEvent.RemoveAllListeners();
    }
}