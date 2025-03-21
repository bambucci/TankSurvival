using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Game Config", menuName = "Scriptable Objects/Game Config", order = 0)]
    public class GameConfig: ScriptableObject
    {
        public UnitConfig PlayerConfig;
        public GameObject LevelPrefab;
        public LayerMask UnitLayer;
    }
}