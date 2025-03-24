using System;
using System.Collections;
using System.Collections.Generic;
using Core.ScriptableObjects;
using Core.Services;
using Core.StateMachines.GameStates;
using Core.Utils;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;
using VContainer;

namespace Core.Managers
{
    public class WaveSpawnManager : MonoBehaviour
    {
        [SerializeField] private WaveComposition[] waveCompositions;
        [SerializeField] private GameObject portalPrefab;
        [SerializeField] private ParticleSystem portalGlow;
        [SerializeField] private float circleSize = 5;
        [SerializeField] private int maxWaveUnitCount = 50;
        [SerializeField] private int waveIterationAddedUnits = 3;

        private UnitFactory _unitFactory;
        private Transform _enemyHolder;
        private ParticleSystem.EmitParams _emitParams;
        private bool interruptSpawn;
        private Coroutine spawnRoutine;

        [Inject]
        public void Construct(UnitFactory unitFactory)
        {
            _unitFactory = unitFactory;
        }

        private void Start()
        {
            _enemyHolder = new GameObject("EnemyHolder").transform;
            _emitParams = new ParticleSystem.EmitParams();
        }

        public void StartSpawning()
        {
            StopAllCoroutines();
            interruptSpawn = false;
            spawnRoutine = StartCoroutine(SpawnWaveRoutine());
        }

        private IEnumerator SpawnWaveRoutine()
        {
            Debug.Log("Spawn Wave Start");
            
            var spawnIndex = 0;
            var waveIteration = 0;

            while (true)
            {
                
                var composition = waveCompositions[spawnIndex];
                composition.Count = Mathf.Clamp(composition.Count + waveIteration * waveIterationAddedUnits, 0, maxWaveUnitCount);
                
                yield return Yielders.Get(composition.DelayAfterPreviousWave);

                var spawnInfos = GetSpawnPositions(composition);

                yield return SpawnUnitsAtPositions(composition, spawnInfos);

                if (spawnIndex + 1 < waveCompositions.Length)
                    spawnIndex++;
                else
                {
                    waveIteration++;
                    spawnIndex = 0;
                }
            }
        }

        private List<SpawnInfo> GetSpawnPositions(WaveComposition composition)
        {
            try
            {
                return composition.SpawnPattern switch
                {
                    SpawnPattern.Wave => GetWaveSpawnPositions(composition.Count, circleSize),
                    SpawnPattern.Circle => GetCircularSpawnPositions(composition.Count, circleSize),
                    _ => throw new ArgumentOutOfRangeException(nameof(composition.SpawnPattern), 
                        $"Unhandled spawn pattern: {composition.SpawnPattern}")
                };
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error generating spawn positions: {ex.Message}");
                return null;
            }
        }

        private List<SpawnInfo> GetWaveSpawnPositions(int unitCount, float radius)
        {
            var spawnInfos = new List<SpawnInfo>();
            var randomRotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

            for (var i = 0; i < unitCount; ++i)
            {
                var angle = 2 * Mathf.PI / (radius * 5) * i;
                var x = Mathf.Cos(angle) * radius;
                var z = Mathf.Sin(angle) * radius;
                var position = transform.position + randomRotation * new Vector3(x, 0, z);
                
                var rotation = Quaternion.LookRotation(transform.position - position);

                spawnInfos.Add(new SpawnInfo(position, rotation));
            }

            return spawnInfos;
        }
        
        private List<SpawnInfo> GetCircularSpawnPositions(int unitCount, float radius)
        {
            var spawnInfos = new List<SpawnInfo>();

            for (var i = 0; i < unitCount; ++i)
            {
                var angle = i * (2 * Mathf.PI / unitCount);
                var x = Mathf.Cos(angle) * radius;
                var z = Mathf.Sin(angle) * radius;
                var position = transform.position + new Vector3(x, 0, z);
        
                var rotation = Quaternion.LookRotation(transform.position - position);

                spawnInfos.Add(new SpawnInfo(position, rotation));
            }

            return spawnInfos;
        }
        
        

        private IEnumerator SpawnUnitsAtPositions(WaveComposition composition, List<SpawnInfo> spawnInfos)
        {
            foreach (var spawnInfo in spawnInfos)
            {
                if (interruptSpawn)
                {
                    interruptSpawn = false;
                    StopCoroutine(spawnRoutine);
                    yield break;
                }

                var portal = GetPortal(spawnInfo.Position, spawnInfo.Rotation);
                _emitParams.position = spawnInfo.Position + Vector3.up;
                portalGlow.Emit(_emitParams, 1);

                portal.SpawnUnitEvent.AddListener(() =>
                {
                    var unit = _unitFactory.CreateEnemyUnit(
                        composition.UnitConfig,
                        spawnInfo.Position,
                        spawnInfo.Rotation,
                        _enemyHolder);
                    unit.transform.DOScaleZ(1, 0.4f).From(0);
                });

                portal.GetSequence();

                if (composition.IndividualSpawnDelay > 0)
                    yield return Yielders.Get(composition.IndividualSpawnDelay);
            }
        }

        private struct SpawnInfo
        {
            public Vector3 Position { get; }
            public Quaternion Rotation { get; }

            public SpawnInfo(Vector3 position, Quaternion rotation)
            {
                Position = position;
                Rotation = rotation;
            }
        }

        public void InterruptSpawning() => interruptSpawn = true;

        private SpawnPortal GetPortal(Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(portalPrefab, position, rotation).GetComponent<SpawnPortal>();
        }
    }


    [Serializable]
    public struct WaveComposition
    {
        public UnitConfig UnitConfig;
        public int Count;
        public SpawnPattern SpawnPattern;
        public float DelayAfterPreviousWave;
        public float IndividualSpawnDelay;
    }

    public enum SpawnPattern
    {
        Wave,
        Circle
    }
}