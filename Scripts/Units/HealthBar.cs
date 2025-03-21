using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Core.Units
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private float healthShotDuration = 5;
        [SerializeField] private float fadeDuration = 1f;

        private static readonly int fill = Shader.PropertyToID("_Fill");
        private static readonly int transparency = Shader.PropertyToID("_Transparency");

        private MaterialPropertyBlock _matBlock;
        private MeshRenderer _meshRenderer;
        private Camera _mainCamera;
        private IHealth _health;

        private bool _changeTriggered;
        private bool _isDisplaying;
        private float _displayTimer;

        private Tween _fadeTween;

        private void Awake()
        {
            InitializeComponents();
        }

        private void Start()
        {
            CacheMainCamera();
        }

        private void Update()
        {
            if (!_changeTriggered) return;
            AlignCamera();
            
            if (!_isDisplaying) return;
            HandleDisplayTimer();
        }

        private void InitializeComponents()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _meshRenderer.enabled = false;
            _matBlock = new MaterialPropertyBlock();
            _meshRenderer.GetPropertyBlock(_matBlock);
            _health = GetComponentInParent<IHealth>();
            _health.HealthChangedEvent.AddListener(OnHealthChanged);
            
            _fadeTween = DOVirtual.Float(1f, 0f, fadeDuration, (value) => {
                _matBlock.SetFloat(transparency, value);
                _meshRenderer.SetPropertyBlock(_matBlock);
            }).OnComplete(DisableHealthBar)
              .SetAutoKill(false)
              .Pause();
        }
        
        private void OnHealthChanged(float health)
        {
            _fadeTween.Pause();
            EnableHealthBar();
            UpdateHealthParameters();
            UpdateTransparency(1f);
            _displayTimer = healthShotDuration;
            _isDisplaying = true;
            _changeTriggered = true;
        }

        private void CacheMainCamera()
        {
            _mainCamera = Camera.main;
        }

        private void EnableHealthBar()
        {
            _meshRenderer.enabled = true;
        }

        private void DisableHealthBar()
        {
            _meshRenderer.enabled = false;
            _changeTriggered = false;
        }

        private void AlignCamera()
        {
            if (!_mainCamera) return;

            var forward = transform.position - _mainCamera.transform.position;
            forward.Normalize();
            var up = Vector3.Cross(forward, _mainCamera.transform.right);
            transform.rotation = Quaternion.LookRotation(forward, up);
        }

        private void UpdateHealthParameters()
        {
            UpdateShaderProperties();
        }

        private void UpdateShaderProperties()
        {
            _matBlock.SetFloat(fill, _health.Health / _health.MaxHealth);
            _meshRenderer.SetPropertyBlock(_matBlock);
        }

        private void HandleDisplayTimer()
        {
            _displayTimer -= Time.deltaTime;

            if (_displayTimer <= 0)
            {
                _isDisplaying = false;
                _fadeTween.Restart();
            }
        }
        
        private void UpdateTransparency(float alpha)
        {
            _matBlock.SetFloat(transparency, alpha);
            _meshRenderer.SetPropertyBlock(_matBlock);
        }

        private void OnDisable()
        {
            DisableHealthBar();
            _isDisplaying = false;
            _fadeTween.Pause();
        }
    }
}