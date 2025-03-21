using System;

namespace Core.Data
{
    [Serializable]
    public class PlayerMovementParameters
    {
        public float Acceleration;
        public float MaxSpeed;
        public float ForwardDamping;
        public float SideDamping;
        public float RotationSpeed;
    }
}