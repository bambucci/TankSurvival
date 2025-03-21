using System;
using Core.ScriptableObjects;
using Core.Services;
using Core.Units;
using Core.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private WeaponConfig weaponConfig;
        [SerializeField] private InputActionReference inputFire;
        [SerializeField] private Transform firePoint;
        private WeaponFactory _weaponFactory;
        private Weapon _weapon;
        private bool IsFiring => inputFire.action.IsPressed();
        
        public void Initialize(WeaponFactory weaponFactory)
        {
            _weaponFactory = weaponFactory;
            _weapon = _weaponFactory.Create(weaponConfig, firePoint, UnitTeam.Player);
        }

        private void Update()
        {
            if (IsFiring) 
                _weapon.Fire();
        }
    }
}