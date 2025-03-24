using Core.Managers;
using Core.Services;
using VContainer.Unity;

namespace Core
{
    public class EntryPoint : IInitializable
    {
        private readonly GameStateManager _gameStateManager;
        
        public EntryPoint(GameStateManager gameStateManager)
        {
            _gameStateManager = gameStateManager;
        }
        
        public void Initialize()
        {
            _gameStateManager.Initialize();
        }
    }
}
