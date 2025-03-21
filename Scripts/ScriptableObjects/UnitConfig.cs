using Core.Data;
using UnityEngine;

namespace Core.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Unit Config", menuName = "Scriptable Objects/Unit Config", order = 0)]
    public class UnitConfig: ScriptableObject
    {
        public GameObject Prefab;
        public float Health;
        
        [Header("Attack")]
        public float AttackDamage;
        public float AttackRange;
        public float AttackRadius;
        [Tooltip("Attacks per second")] 
        public float AttackSpeed;
        public float KnockbackForce;
        
    }
}