using Core.Services;
using VContainer.Unity;

namespace Core
{
    public class GameStarter : IInitializable
    {
        private readonly LevelManager _levelManager;
        
        public GameStarter(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }
        
        public void Initialize()
        {
            _levelManager.Initialize();
        }
    }
}
