using UnityEngine;

namespace Core.Player.Vehicle
{
    public abstract class Chasis: MonoBehaviour
    {
        [field: SerializeField] public Transform TurretAttachPoint { get; private set; }
        public abstract void AnimateMovement();
    }
}