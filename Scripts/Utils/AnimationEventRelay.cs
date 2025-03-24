using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Core.Utils
{
	public class AnimationEventRelay : MonoBehaviour
	{
		[SerializeField] private UnityEvent attackEvent;

		private void OnAttack()
		{
			attackEvent.Invoke();
		}
	}
}
