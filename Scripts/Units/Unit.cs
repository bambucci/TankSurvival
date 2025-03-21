using UnityEngine;
using UnityEngine.Events;

namespace Core.Units
{
    public abstract class Unit : MonoBehaviour, IUnit
    {
        public float Health { get; protected set; }
        public float MaxHealth { get; protected set; } = 100;
        public abstract UnitTeam Team { get; }
        public Vector3 Position => transform.position;
        public UnityEvent<float> HealthChangedEvent { get; } = new();
        public readonly UnityEvent DeathEvent = new();
        
        public virtual void TakeDamage(float damage)
        {
            if (damage < 0) return;
            Health = Mathf.Max(Health - damage, 0);
            HealthChangedEvent?.Invoke(Health);
            
            if (Health <= 0)
            {
                DeathEvent?.Invoke();
            }
        }

        public void Heal(float amount)
        {
            if (amount < 0) return;
            Health = Mathf.Min(Health + amount, MaxHealth);
            HealthChangedEvent?.Invoke(Health);
        }

        public virtual void Kill(bool withFX = true)
        {
            Health = 0;
            DeathEvent?.Invoke();
        }
        
        public virtual void ApplyKnockback(Vector3 knockbackVector)
        {
            
        }
    }
    
    public enum UnitTeam
    {
        Player,
        Enemy
    }
}