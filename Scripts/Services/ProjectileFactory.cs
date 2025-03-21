using Core.Weapons;
using UnityEngine;
using Lean.Pool;

namespace Core.Services
{
    public class ProjectileFactory
    {
        private readonly Transform _projectileParent = new GameObject("Projectiles").transform;
        public Projectile CreateProjectile(GameObject projectilePrefab, Vector3 position, Quaternion rotation)
        {
            var projectile = LeanPool.Spawn(projectilePrefab, position, rotation, _projectileParent).GetComponent<Projectile>();
            return projectile;
        }
    }
}