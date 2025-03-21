using System;
using Cinemachine;
using Core.Data;
using Core.Player;
using UnityEngine;
using VContainer;

namespace Core.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private int targetFrameRate = 60;
        [SerializeField] private bool depthTexture;
        private GameplayData _gameplayData;

        [Inject]
        private void Construct(GameplayData gameplayData)
        {
            _gameplayData = gameplayData;
        }
        
        private void Start()
        {
            Application.targetFrameRate = targetFrameRate;
            mainCamera.depthTextureMode = depthTexture ? DepthTextureMode.Depth : DepthTextureMode.None;
            virtualCamera.m_Follow = _gameplayData.PlayerController.transform;
        }

        private void OnValidate()
        {
            mainCamera.depthTextureMode = depthTexture ? DepthTextureMode.Depth : DepthTextureMode.None;
        }
    }
}
