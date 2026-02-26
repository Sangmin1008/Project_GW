using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using VContainer;

public class WeaponView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Transform cameraTransform;

    public PlayerInput Input;
    
    public IObservable<Unit> OnFireRequested => Observable
        .EveryUpdate()
        .Where(_ => Input.Player.Attack.IsPressed())
        .Select(_ => Unit.Default);
    
    public IObservable<Unit> OnReloadRequested => Observable
        .EveryUpdate()
        .Where(_ => Input.Player.Reload.WasPressedThisFrame())
        .Select(_ => Unit.Default);
    
    public IObservable<bool> OnAimInput => Observable
        .EveryUpdate()
        .Where(_ => Input.Player.Aim.WasPressedThisFrame())
        .Scan(false, (isAiming, _) => !isAiming);
    
    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        Input = playerInput;
    }
    
    void OnEnable() => Input?.Enable();
    void OnDisable() => Input?.Disable();

    public void UpdateAmmoUI(int ammo, int maxAmmo)
    {
        ammoText.text = $"Ammo: {ammo} / {maxAmmo}";
    }
    
    public void PerformHitscan(WeaponConfig config)
    {
        if (muzzleFlash != null)
        {
            muzzleFlash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            muzzleFlash.Play();
        }

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, config.Range))
        {
            
        }
    }
}
