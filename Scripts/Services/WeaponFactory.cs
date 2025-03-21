using System;
using System.Collections.Generic;
using Core.ScriptableObjects;
using Core.Units;
using Core.Weapons;
using UnityEngine;

namespace Core.Services
{
    public class WeaponFactory
    {
        private readonly Dictionary<Type, Func<WeaponConfig, Transform, UnitTeam, Weapon>> _creators
            = new();
        
        public WeaponFactory(ProjectileFactory projectileFactory, DamageDealer damageDealer)
        {
            Register<ProjectileWeaponConfig>((config, fp, team) => 
                new ProjectileWeapon((ProjectileWeaponConfig)config, damageDealer, fp, team, projectileFactory));
        }

        private void Register<T>(Func<WeaponConfig, Transform, UnitTeam, Weapon> creator) 
            where T : WeaponConfig
        {
            _creators[typeof(T)] = creator;
        }

        public Weapon Create(WeaponConfig config, Transform firePoint, UnitTeam team)
        {
            if (config == null)
            {
                Debug.LogError("Config is null!");
                return null;
            }

            var configType = config.GetType();
            if (_creators.TryGetValue(configType, out var creator))
                return creator(config, firePoint, team);

            Debug.LogError($"No weapon registered for config type: {configType}");
            return null;
        }
    }
}