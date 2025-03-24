using UnityEngine;

namespace Core.UI
{
    public class UIManager: MonoBehaviour
    {
        [field:SerializeField] public GameOverScreenUI GameOverScreen { get; private set; }
    }
}