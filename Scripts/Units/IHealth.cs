using UnityEngine.Events;

namespace Core.Units
{
    public interface IHealth
    {
        float Health { get; }
        float MaxHealth { get; }
        
        UnityEvent<float> HealthChangedEvent { get; }

        void TakeDamage(float damage);
        void Heal(float amount);
        void Kill(bool withFX);
    }
}