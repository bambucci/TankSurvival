using UnityEngine;

namespace Core.Units
{
    public interface IUnit : IKnockbackable, IHealth
    {
        UnitTeam Team { get; }
        Vector3 Position { get; }
    }
}