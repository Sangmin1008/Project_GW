using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Cinemachine;
using UnityEngine;

[Serializable]
public struct CameraShakeData
{
    public CameraShakeType ShakeType;
    public CinemachineImpulseSource ImpulseSource;
}

public class CameraSystemView : MonoBehaviour, ICameraSystem
{
    [Header("Cinemachine References")]
    [SerializeField] private CinemachineCamera _playerVCam;

    [Header("Zoom Settings")]
    [SerializeField] private float _defaultFOV = 60f;
    [SerializeField] private float _zoomSpeed = 15f;

    [Header("Shake Settings")]
    [SerializeField] private List<CameraShakeData> _shakeDataList;
    private Dictionary<CameraShakeType, CinemachineImpulseSource> _shakeSources;
    
    private CancellationTokenSource _zoomCts;

    private void Awake()
    {
        _shakeSources = new Dictionary<CameraShakeType, CinemachineImpulseSource>();
        foreach (var data in _shakeDataList)
        {
            if (data.ImpulseSource != null && !_shakeSources.ContainsKey(data.ShakeType))
                _shakeSources.Add(data.ShakeType, data.ImpulseSource);
        }

        if (_playerVCam != null)
            _playerVCam.Lens.FieldOfView = _defaultFOV;
    }

    public void PlayShake(CameraShakeType type, float intensityMultiplier = 1f)
    {
        if (_shakeSources.TryGetValue(type, out var source))
        {
            source.GenerateImpulseWithForce(intensityMultiplier);
        }
    }

    public void SetAimState(bool isAiming, float targetFOV = 40f)
    {
        float finalFOV = isAiming ? targetFOV : _defaultFOV;

        _zoomCts?.Cancel();
        _zoomCts = new CancellationTokenSource();

        ChangeFOVAsync(finalFOV, _zoomCts.Token).Forget();
    }

    private async UniTaskVoid ChangeFOVAsync(float targetFOV, CancellationToken token)
    {
        if (_playerVCam == null) return;

        try
        {
            while (Mathf.Abs(_playerVCam.Lens.FieldOfView - targetFOV) > 0.1f)
            {
                _playerVCam.Lens.FieldOfView = Mathf.Lerp(
                    _playerVCam.Lens.FieldOfView, 
                    targetFOV, 
                    Time.deltaTime * _zoomSpeed
                );

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken: token);
            }

            _playerVCam.Lens.FieldOfView = targetFOV;
        }
        catch (OperationCanceledException)
        {
        }
    }

    private void OnDestroy()
    {
        _zoomCts?.Cancel();
        _zoomCts?.Dispose();
    }
}