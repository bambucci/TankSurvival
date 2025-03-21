using System.Collections.Generic;

namespace Core.Utils.Timer
{
    public static class TimerManager
    {
        private static readonly List<Timer> timers = new();
        
        public static void RegisterTimer(Timer timer) => timers.Add(timer);
        public static void DeregisterTimer(Timer timer) => timers.Remove(timer);

        public static void UpdateTimers()
        {
            if (timers.Count == 0) return;

            for (var i = 0; i < timers.Count; i++)
            {
                timers[i].Tick();
            }
        }
    }
}