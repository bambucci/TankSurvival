using Core.ScriptableObjects;
using Core.Units;
using UnityEngine;
using Lean.Pool;
using UnityEngine.Serialization;

namespace Core.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float size = 0.1f;
        private ProjectileWeaponConfig _config;
        private Weapon _weapon;
        private Vector3 _direction;
        private float _spawnTime;

        public void Initialize(ProjectileWeaponConfig config, Weapon weapon, Vector3 direction)
        {
            _config = config;
            _weapon = weapon;
            _direction = direction.normalized;
            _spawnTime = Time.time;
        }

        private void Update()
        {
            CheckCollisions();
            Move();

            if (Time.time - _spawnTime >= 5)
            {
                Despawn();
            }
        }

        protected virtual void Move()
        {
            transform.position += _direction * (_config.ProjectileSpeed * Time.deltaTime);
        }

        private void CheckCollisions()
        {
            if (Physics.SphereCast(transform.position,size, _direction, out var hit, _config.ProjectileSpeed * Time.deltaTime))
                HitDetected(hit);
        }

        private void HitDetected(RaycastHit hit)
        {
            _weapon.OnHit(hit.point, hit.collider.GetComponent<IHealth>());
            
            if (_config.HitFX) 
                LeanPool.Spawn(_config.HitFX, hit.point - _direction.normalized * size, Quaternion.identity);
            
            Despawn();
        }

        private void Despawn()
        {
            LeanPool.Despawn(gameObject);
        }
    }
}