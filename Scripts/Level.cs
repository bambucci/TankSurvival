using System;
using Unity.AI.Navigation;
using UnityEngine;

namespace Core
{
    public class Level : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerSpawnPoint { get; private set; }
    }
}