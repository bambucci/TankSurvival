using Core.Data;
using Core.Services;
using Core.UI;
using UnityEngine;

namespace Core.StateMachines.GameStates
{
    public class GameOverState : IState
    {
        private readonly GameplayData _gameplayData;
        private readonly LevelManager _levelManager;
        private readonly UIManager _uiManager;
        public bool RestartTriggered { get; private set; }
        public GameOverState(GameplayData gameplayData, 
                             LevelManager levelManager,
                             UIManager uiManager)
        {
            _gameplayData = gameplayData;
            _levelManager = levelManager;
            _uiManager = uiManager;
        }
        public void OnEnter()
        {
            _uiManager.GameOverScreen.Show(_gameplayData.CurrentScore, _gameplayData.HighScore, 0);
            _levelManager.LevelEnd();
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void OnExit()
        {
            _gameplayData.Clear();
            RestartTriggered = false;
        }

        public void TriggerRestart()
        {
            RestartTriggered = true;
        }
    }
}