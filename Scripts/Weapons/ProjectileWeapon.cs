using Core.ScriptableObjects;
using Core.Services;
using Core.Units;
using UnityEngine;
using VContainer;

namespace Core.Weapons
{
    public class ProjectileWeapon : Weapon<ProjectileWeaponConfig>
    {
        private readonly ProjectileFactory _projectileFactory;
        private float _lastFireTime;
        
        public ProjectileWeapon(ProjectileWeaponConfig config,
                                DamageDealer damageDealer,
                                Transform firePoint, 
                                UnitTeam team,
                                ProjectileFactory projectileFactory) : base(config, firePoint, damageDealer, team)
        {
            _projectileFactory = projectileFactory;
        }
        
        public override void Fire()
        {
            if (Time.time < _lastFireTime + 1f / Config.AttackSpeed)
                return;

            float angleStep = Config.ArcAngle / (Config.BulletCount - 1);
            float startAngle = -Config.ArcAngle / 2f;
            
            for (int i = 0; i < Config.BulletCount; i++)
            {
                var angle = startAngle + angleStep * i;
                var direction = Quaternion.Euler(0, angle, 0) * FirePoint.forward;
                
                var projectile = 
                    _projectileFactory.CreateProjectile(Config.ProjectilePrefab, FirePoint.position, Quaternion.identity);
                projectile.Initialize(Config, this, direction);
            }
            
            _lastFireTime = Time.time;
        }
        public override void OnHit(Vector3 position, IHealth health = null)
        {
            DamageDealer.DealAreaDamage(Config.BaseDamage, position ,0.5f, Config.KnockbackForce);
        }
    }
}