using UnityEngine;

namespace Core.Utils.Timer
{
    public class TimerUpdater : MonoBehaviour
    {
        private void Update()
        {
            TimerManager.UpdateTimers();
        }
    }
}