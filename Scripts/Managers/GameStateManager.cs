using Core.Data;
using Core.Services;
using Core.StateMachines;
using Core.StateMachines.GameStates;
using Core.UI;
using UnityEngine;
using VContainer;

namespace Core.Managers
{
    public class GameStateManager : MonoBehaviour
    {
        private GameplayData _gameplayData;
        private StateMachine _stateMachine;
        private LevelManager _levelManager;
        private UIManager _uiManager;
        
        [Inject]
        public void Construct(GameplayData gameplayData, LevelManager levelManager, UIManager uiManager)
        {
            _gameplayData = gameplayData;
            _levelManager = levelManager;
            _uiManager = uiManager;
            
            _stateMachine = new StateMachine();
        }

        public void Initialize()
        {
            var initState = new InitState();
            var gameplayState = new GameplayState(_gameplayData, _levelManager);
            var gameOverState = new GameOverState(_gameplayData, _levelManager, _uiManager);
            
            At(initState, gameplayState, new FuncPredicate(() => true));
            At(gameplayState, gameOverState,  new FuncPredicate(() => !_gameplayData.PlayerController));
            At(gameOverState, gameplayState,  new FuncPredicate(() => gameOverState.RestartTriggered));
            
            _uiManager.GameOverScreen.RestartButtonPressed.AddListener(gameOverState.TriggerRestart);
            
            _stateMachine.SetState(initState);
        }
        
        private void Update() => _stateMachine?.Update();
        private void At(IState from, IState to, IPredicate condition) =>
            _stateMachine.AddTransition(from, to, condition);
        private void Any(IState to, IPredicate condition) =>
            _stateMachine.AddAnyTransition(to, condition);
    }
}