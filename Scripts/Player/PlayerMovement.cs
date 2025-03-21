
using System;
using Core.Data;
using Core.Stats;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Core.Player
{
    public class PlayerMovement: MonoBehaviour
    {
        [FormerlySerializedAs("movementParams")] [SerializeField] private PlayerMovementParameters movementParameters;
        [SerializeField] private InputActionReference inputMove;
        [SerializeField] private Quaternion levelRotation;
        [SerializeField] private TrailRenderer[] trailRenderers;
        [SerializeField] private ParticleSystem[] trailSmokes;
        [SerializeField] private float skidmarkAngleThreshold = 30;
        [SerializeField] private float skidmarkSpeedThreshold = 2;
        private Rigidbody _rigidbody;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Initialize(StatHandler statHandler)
        {
            var maxSpeedStat = statHandler.GetStat(BaseStat.MoveMaxSpeed);
            var accelerationStat = statHandler.GetStat(BaseStat.MoveAcceleration);
            
            movementParameters.MaxSpeed = maxSpeedStat.BaseValue;
            movementParameters.Acceleration = accelerationStat.BaseValue;

            maxSpeedStat.OnStatChanged += OnMaxSpeedStatChange;
            accelerationStat.OnStatChanged += OnAccelerationStatChange;
        }

        private void Update()
        {
            _moveDirection = levelRotation * inputMove.action.ReadValue<Vector2>().AsXZ();
            Skidmarks();
        }

        private void FixedUpdate()
        {
            ApplyDamping();
            
            if (_moveDirection.magnitude < 0.1f) return;
            
            RotateBase();
            Move();
        }

        private void Move()
        {
            var lerpedSpeed = Mathf.InverseLerp(125, 0, AngleDifference());
            var moveForce = transform.forward * (movementParameters.Acceleration * lerpedSpeed);
            _rigidbody.AddForce(moveForce, ForceMode.Acceleration);
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, movementParameters.MaxSpeed);
        }

        private void RotateBase()
        {
            var sign = AngleDifference() < 190 ? 1 : -1;
            var rotateDirection = Quaternion.LookRotation(_moveDirection * sign, Vector3.up);
            var targetRotation = Quaternion.RotateTowards(_rigidbody.rotation, rotateDirection, movementParameters.RotationSpeed * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(targetRotation);
        }

        private void ApplyDamping()
        {
            if (_rigidbody.velocity.magnitude < 0.01f) return;
            var velocity = transform.InverseTransformDirection(_rigidbody.velocity);

            _rigidbody.AddForce(transform.right * (-velocity.x * movementParameters.SideDamping) , ForceMode.Acceleration);
            _rigidbody.AddForce(transform.forward * (-velocity.z * movementParameters.ForwardDamping) , ForceMode.Acceleration);
        }

        private float AngleDifference()
        {
            var angleDifference = Vector3.Angle(_moveDirection, _rigidbody.transform.forward);
            return angleDifference;
        }

        private void Skidmarks()
        {
            var localVelocity = transform.InverseTransformDirection(_rigidbody.velocity);
            var shouldEmit = Mathf.Abs(localVelocity.x) > skidmarkAngleThreshold && 
                             _rigidbody.velocity.magnitude > skidmarkSpeedThreshold;

            if (shouldEmit == trailRenderers[0].emitting) return;
            
            foreach (var trail in trailRenderers)
            {
                trail.emitting = shouldEmit;
            }
            foreach (var particleSmoke in trailSmokes)
            {
                var emission = particleSmoke.emission;
                emission.enabled = shouldEmit;
            }
        }

        public void ApplyForce(Vector3 force, ForceMode forceMode = ForceMode.VelocityChange)
        {
            _rigidbody.AddForce(force, forceMode);
        }
        
        private void OnMaxSpeedStatChange(float newMaxSpeed) => 
            movementParameters.MaxSpeed = newMaxSpeed;
        
        private void OnAccelerationStatChange(float newAcceleration) => 
            movementParameters.Acceleration = newAcceleration;
    }
}