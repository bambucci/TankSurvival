using Core.Data;
using Core.Managers;
using Core.Services;

namespace Core.StateMachines.GameStates
{
    public class GameplayState : IState
    {
        private readonly GameplayData _gameplayData;
        private readonly LevelManager _levelManager;
        public GameplayState(GameplayData gameplayData, 
                             LevelManager levelManager)
        {
            _gameplayData = gameplayData;
            _levelManager = levelManager;
        }
        
        public void OnEnter()
        {
            _levelManager.LevelStart();
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}