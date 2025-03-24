using Core.Data;
using Core.Managers;
using Core.ScriptableObjects;
using Core.Services;
using Core.Stats;
using Core.UI;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameConfig gameConfig;
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private WaveSpawnManager waveSpawnManager;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private PopUpTextManager popUpTextManager;
        [SerializeField] private GameStateManager gameStateManager;
        [SerializeField] private UIManager uiManager;
        
        protected override void Configure(IContainerBuilder builder)
        {
            Data(builder);
            UnityObjects(builder);
            Services(builder);
        }

        private static void Services(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<EntryPoint>();
            builder.Register<LevelManager>(Lifetime.Singleton);
            builder.Register<UnitFactory>(Lifetime.Singleton);
            builder.Register<WeaponFactory>(Lifetime.Singleton);
            builder.Register<ProjectileFactory>(Lifetime.Singleton);
            builder.Register<DamageDealer>(Lifetime.Singleton);
            builder.Register<StatHandler>(Lifetime.Singleton);
        }

        private static void Data(IContainerBuilder builder)
        {
            builder.Register<GameplayData>(Lifetime.Singleton);
        }

        private void UnityObjects(IContainerBuilder builder)
        {
            builder.RegisterComponent(cameraManager);
            builder.RegisterComponent(gameConfig);
            builder.RegisterComponent(waveSpawnManager);
            builder.RegisterComponent(inputManager);
            builder.RegisterComponent(popUpTextManager);
            builder.RegisterComponent(gameStateManager);
            builder.RegisterComponent(uiManager);
        }
    }
}