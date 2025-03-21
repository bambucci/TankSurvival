using Core.Utils.Timer;

namespace Core.Stats
{
    public class StatModifier
    {
        public float Value { get; }
        public ModifierType Type { get; }
        public Timer Timer { get; }
        
        public StatModifier(float value, ModifierType type, Timer timer = null)
        {
            Value = value;
            Type = type;
            Timer = timer;
        }
        
    }
}