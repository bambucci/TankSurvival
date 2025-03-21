using System;
using System.Collections;
using Core.ScriptableObjects;
using Core.Services;
using Core.Utils;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;
using VContainer;

namespace Core.Managers
{
    public class WaveSpawnManager: MonoBehaviour
    {
        [SerializeField] private WaveComposition[] waveCompositions;
        [SerializeField] private GameObject portalPrefab;
        [SerializeField] private ParticleSystem portalGlow;
        [SerializeField] private float circleSize = 5;
        private UnitFactory _unitFactory;
        private int _currentWaveIndex;
        private Transform _enemyHolder;
        private ParticleSystem.EmitParams _emitParams;

        [Inject]
        public void Construct(UnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        private void Start()
        {
            _enemyHolder = new GameObject("EnemyHolder").transform;
            _emitParams = new ParticleSystem.EmitParams();
            StartCoroutine(SpawnWaveCircle());
        }

        private IEnumerator SpawnWaveCircle()
        {
            foreach (var composition in waveCompositions)
            {
                yield return Yielders.Get(composition.DelayAfterPreviousWave);
                
                var randomRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
                for (var i = 0; i < composition.Count; ++i)
                {
                    var theta = 2 * Mathf.PI / (circleSize * 5) * i;
                    var addedVector = new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));
                    var newVector = transform.position + randomRotation * (addedVector * circleSize);
                    var lookRotation = Quaternion.LookRotation(transform.position - newVector);

                    var portal = GetPortal(newVector, lookRotation);
                    _emitParams.position = newVector + Vector3.up;
                    portalGlow.Emit(_emitParams, 1);
                    portal.SpawnUnitEvent.AddListener(() =>
                    {
                        var unit = _unitFactory.CreateEnemyUnit(composition.UnitConfig, 
                            newVector, 
                            lookRotation,
                            _enemyHolder);
                        unit.transform.DOScaleZ(1, 0.4f).From(0);
                    });
                    portal.GetSequence();
                    
                    yield return Yielders.Get(composition.IndividualSpawnDelay);
                }
            }
        }

        private SpawnPortal GetPortal(Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(portalPrefab, position, rotation).GetComponent<SpawnPortal>();
        }
    }


    [Serializable]
    public class WaveComposition
    {
        public UnitConfig UnitConfig;
        public int Count;
        public SpawnPattern SpawnPattern;
        public float DelayAfterPreviousWave;
        public float IndividualSpawnDelay;
    }
    
    public enum SpawnPattern { Random, Wave, Circle }
}