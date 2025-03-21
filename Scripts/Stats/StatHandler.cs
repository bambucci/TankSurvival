using System.Collections.Generic;
using System.Linq;
using Core.Utils.Timer;

namespace Core.Stats
{
    public class StatHandler
    {
        private readonly Dictionary<BaseStat, Stat> _stats = new();
        public Stat GetStat(BaseStat type) => _stats.GetValueOrDefault(type);

        public StatHandler()
        {
            _stats.Add(BaseStat.Health, new Stat(100));
            _stats.Add(BaseStat.MoveMaxSpeed, new Stat(8));
            _stats.Add(BaseStat.MoveAcceleration, new Stat(15));
        }

        public void AddModifier(BaseStat statType, float value, ModifierType modifierType, float duration = 0)
        {
            var stat = GetStat(statType);
            if (stat == null) return;
            
            Timer timer = null;

            if (modifierType == ModifierType.TemporaryBuff)
            {
                timer = TryGetTimer(stat, duration);
                if (timer == null) return;
            }
            
            var modifier = new StatModifier(value, modifierType, timer);
            stat.Modifiers.Add(modifier);
            stat.OnStatChanged.Invoke(GetValueFromStat(statType));

            if (timer != null)
                timer.OnTimerStop += () =>
                {
                    stat.Modifiers.Remove(modifier);
                    stat.OnStatChanged.Invoke(GetValueFromStat(statType));
                };
        }

        private Timer TryGetTimer(Stat stat, float duration)
        {
            var existingModifier = stat.Modifiers.LastOrDefault(x => x.Type == ModifierType.TemporaryBuff);
            if (existingModifier != null)
            {
                existingModifier.Timer.Extend(duration);
                return null;
            }

            var timer = new Timer(duration);
            timer.Start();

            return timer;
        }
        
        public float GetValueFromStat(BaseStat statType)
        {
            var stat = GetStat(statType);
            
            var additiveSum = 0f;
            var multiplySum = 1f;
            var buffSum = 1f;

            foreach (var modifier in stat.Modifiers)
            {
                switch (modifier.Type)
                {
                    case ModifierType.Additive:
                        additiveSum += modifier.Value;
                        break;
                    case ModifierType.Multiply:
                        multiplySum += modifier.Value;
                        break;
                    case ModifierType.TemporaryBuff:
                        buffSum += modifier.Value;
                        break;
                }
            }

            return (stat.BaseValue + additiveSum) * multiplySum * buffSum;
        }
    }
}