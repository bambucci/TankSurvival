using System;
using Core.Player.Vehicle;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Player
{
    public class PlayerTurret : MonoBehaviour
    {
        [SerializeField] private InputActionReference mousePositionInput;
        [SerializeField] private Turret turret;
        [SerializeField] private float rotationSpeed = 30f;
        private Camera _camera;
        private Quaternion lastParentRotation;

        private void Awake()
        {
            _camera = Camera.main;
            lastParentRotation = transform.localRotation;
        }

        private void Update()
        {
            RotateTurretTowardsMouse();
        }

        private void RotateTurretTowardsMouse()
        {
            Vector2 mouseScreenPosition = mousePositionInput.action.ReadValue<Vector2>();

            Ray mouseRay = _camera.ScreenPointToRay(mouseScreenPosition);
            Plane groundPlane = new Plane(Vector3.up, turret.transform.position);

            if (groundPlane.Raycast(mouseRay, out float rayDistance))
            {
                turret.transform.localRotation = Quaternion.Inverse(transform.localRotation) * turret.transform.localRotation * lastParentRotation;
                
                lastParentRotation = transform.localRotation;
                Vector3 targetPosition = mouseRay.GetPoint(rayDistance);
                Vector3 direction = (targetPosition - turret.transform.position).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                turret.transform.rotation = Quaternion.RotateTowards(turret.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}