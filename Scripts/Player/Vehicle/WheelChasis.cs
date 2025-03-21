using UnityEngine;

namespace Core.Player.Vehicle
{
    public class WheelChasis: Chasis
    {
        [field: SerializeField] public Transform[] Wheels { get; private set; }

        public override void AnimateMovement()
        {
            
        }
    }
}