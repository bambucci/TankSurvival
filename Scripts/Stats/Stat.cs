using System;
using System.Collections.Generic;

namespace Core.Stats
{
    public class Stat
    {
        public readonly List<StatModifier> Modifiers = new();
        public float BaseValue { get; }
        public Action<float> OnStatChanged = delegate { };
        public Stat(float baseValue) => 
            BaseValue = baseValue;
    }
}