using System.Collections.Generic;
using UnityEngine;

namespace Core.Utils
{
    public static class Yielders
    {
        private static readonly Dictionary<float, WaitForSeconds> timeInterval = new(100);
        private static readonly Dictionary<float, WaitForSecondsRealtime> unscaledTimeInterval = new(100);

        public static WaitForEndOfFrame EndOfFrame { get; } = new();

        public static WaitForFixedUpdate FixedUpdate { get; } = new();

        public static WaitForSeconds Get(float seconds)
        {
            if (!timeInterval.ContainsKey(seconds))
                timeInterval.Add(seconds, new WaitForSeconds(seconds));
            return timeInterval[seconds];
        }

        public static WaitForSecondsRealtime GetUnscaled(float seconds)
        {
            if (!unscaledTimeInterval.ContainsKey(seconds))
                unscaledTimeInterval.Add(seconds, new WaitForSecondsRealtime(seconds));
            return unscaledTimeInterval[seconds];
        }
    }
}