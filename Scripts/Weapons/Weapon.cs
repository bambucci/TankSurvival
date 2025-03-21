using Core.ScriptableObjects;
using Core.Services;
using Core.Units;
using UnityEngine;

namespace Core.Weapons
{
    public abstract class Weapon
    {
        public abstract void Fire();
        public abstract void OnHit(Vector3 position, IHealth health = null);
    }
    
    public abstract class Weapon<T> : Weapon where T : WeaponConfig
    {
        protected T Config { get; }
        protected Transform FirePoint { get; }
        public UnitTeam Team { get; }
        protected DamageDealer DamageDealer;

        protected Weapon(T config, Transform firePoint, DamageDealer damageDealer, UnitTeam team)
        {
            Config = config;
            DamageDealer = damageDealer;
            FirePoint = firePoint;
            Team = team;
        }
    }
}