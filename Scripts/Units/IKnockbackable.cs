using UnityEngine;

namespace Core.Units
{
    public interface IKnockbackable
    {
        void ApplyKnockback(Vector3 knockbackVector);
    }
}