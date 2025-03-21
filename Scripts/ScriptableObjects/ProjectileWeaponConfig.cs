using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Projectile Weapon Config", menuName = "Scriptable Objects/Projectile Weapon Config", order = 0)]
    public class ProjectileWeaponConfig : WeaponConfig
    {
        public GameObject ProjectilePrefab;
        public float ProjectileSpeed;
        public float ArcAngle;
        public int BulletCount;
    }
}