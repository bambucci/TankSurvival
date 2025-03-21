using Core.Data;
using UnityEngine;

namespace Core.ScriptableObjects
{
    public abstract class WeaponConfig : ScriptableObject
    {
        public float BaseDamage;
        public float AttackSpeed;
        public float KnockbackForce;
        public GameObject TurretPrefab;
        public GameObject HitFX;
    }
}