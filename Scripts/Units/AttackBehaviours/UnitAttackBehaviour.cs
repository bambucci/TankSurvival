using Core.Player;
using Core.ScriptableObjects;
using Core.Services;
using UnityEngine;

namespace Core.Units.AttackBehaviours
{
    public abstract class UnitAttackBehaviour: MonoBehaviour
    {
        public abstract void Initialize(UnitConfig config, DamageDealer damageDealer, PlayerController player);
        public abstract void Attack();
    }
}