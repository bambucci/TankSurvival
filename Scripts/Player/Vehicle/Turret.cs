using UnityEngine;

namespace Core.Player.Vehicle
{
    public class Turret: MonoBehaviour
    {
        [field: SerializeField] public Transform FirePoint { get; private set; }
    }
}