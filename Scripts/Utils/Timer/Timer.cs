using System;
using UnityEngine;

namespace Core.Utils.Timer
{
    public class Timer
    {
        public float CurrentTime { get; protected set; }
        public bool IsRunning { get; private set; }

        protected float InitialTime;

        public float Progress => Mathf.Clamp(CurrentTime / InitialTime, 0, 1);

        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        public Timer(float value)
        {
            InitialTime = value;
        }

        public void Start()
        {
            CurrentTime = InitialTime;
            
            if (IsRunning) return;
            
            IsRunning = true;
            TimerManager.RegisterTimer(this);
            OnTimerStart.Invoke();
        }

        public void Stop()
        {
            if (!IsRunning) return;
            
            IsRunning = false;
            TimerManager.DeregisterTimer(this);
            OnTimerStop.Invoke();
        }

        public void Extend(float additionalTime)
        {
            InitialTime += additionalTime;
            CurrentTime += additionalTime;
        }
        
        public void Tick() {
            if (IsRunning && CurrentTime > 0) {
                CurrentTime -= Time.deltaTime;
            }

            if (IsRunning && CurrentTime <= 0) {
                Stop();
            }
        }

        public bool IsFinished => CurrentTime <= 0;

        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;

        public void Reset() => CurrentTime = InitialTime;

        public void Reset(float newTime)
        {
            InitialTime = newTime;
            Reset();
        }
    }
}