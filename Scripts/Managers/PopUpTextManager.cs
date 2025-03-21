using UnityEngine;

namespace Core.Managers
{
    public class PopUpTextManager : MonoBehaviour
    {
        [SerializeField] private DynamicTextData damageTextData;
        [SerializeField] private float damageTextYOffset = 2;

        public void Spawn(Vector3 position, string text)
        {
            DynamicTextManager.CreateText(position + Vector3.up * damageTextYOffset, text, damageTextData);
        }
    }
}
