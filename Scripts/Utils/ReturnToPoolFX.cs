using Lean.Pool;
using UnityEngine;

namespace Core.Utils
{
	public class ReturnToPoolFX : MonoBehaviour
	{
		[SerializeField] private bool returnParent;
		public void OnParticleSystemStopped()
		{
			LeanPool.Despawn(returnParent ? transform.parent.gameObject : gameObject);
		}
	}
}
