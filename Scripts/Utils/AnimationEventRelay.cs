using UnityEngine;
using UnityEngine.Events;

namespace Core.Utils
{
	public class AnimationEventRelay : MonoBehaviour
	{
		[SerializeField] private UnityEvent finishEvent;

		private void OnAttack()
		{
			finishEvent.Invoke();
		}
	}
}
