using System;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Managers
{
    public class SpawnPortal : MonoBehaviour
    {
        private Sequence _spawnPortalSequence;
        private GameObject _portalMesh;
        [NonSerialized]
        public readonly UnityEvent SpawnUnitEvent = new();
        
        public Sequence GetSequence()
        {
            if (_spawnPortalSequence == null)
            {
                _portalMesh = transform.GetChild(0).gameObject;
                _spawnPortalSequence = DOTween.Sequence();
                _spawnPortalSequence.AppendCallback(() => SwitchPortalMesh(true));
                _spawnPortalSequence.Append(_portalMesh.transform.DOScaleX(1, 0.2f).From(0).SetEase(Ease.Linear));
                _spawnPortalSequence.AppendCallback(() => SpawnUnitEvent.Invoke());
                _spawnPortalSequence.AppendInterval(1);
                _spawnPortalSequence.Append(_portalMesh.transform.DOScaleY(0, 0.1f).From(1).SetEase(Ease.Linear));
                _spawnPortalSequence.AppendCallback(() => SwitchPortalMesh(false));
                _spawnPortalSequence.SetAutoKill(false);
            }
            else
            {
                _spawnPortalSequence.Restart();
            }
            //glowFX.Play();
            return _spawnPortalSequence;
        }
        
        private void SwitchPortalMesh(bool active)
        {
            _portalMesh.SetActive(active);
            
            if (!active)
                LeanPool.Despawn(gameObject);
        }

        private void OnDisable()
        {
            SpawnUnitEvent.RemoveAllListeners();
        }
    }
}