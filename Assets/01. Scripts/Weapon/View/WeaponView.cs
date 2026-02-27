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
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private Transform cameraTransform;
    
    [Header("Crosshair UI")]
    [SerializeField] private Image crosshairDot;
    [SerializeField] private Image crosshairOuter;

    private RectTransform _outerRect;
    private Vector2 _targetOuterSize;
    private float _shrinkSpeed;

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

    private void Awake()
    {
        if (crosshairOuter != null)
            _outerRect = crosshairOuter.rectTransform;
    }
    
    void Start()
    {
        Observable.EveryUpdate()
            .Where(_ => _outerRect != null)
            .Where(_ => (_outerRect.sizeDelta - _targetOuterSize).sqrMagnitude > 0.01f) 
            .Subscribe(_ =>
            {
                _outerRect.sizeDelta = Vector2.Lerp(
                    _outerRect.sizeDelta, 
                    _targetOuterSize, 
                    Time.deltaTime * _shrinkSpeed
                );
            })
            .AddTo(this);
    }
    
    void OnEnable() => Input?.Enable();
    void OnDisable() => Input?.Disable();
    
    public void SetWeaponName(string weaponName) => weaponNameText.text = weaponName;

    public void UpdateAmmoUI(int ammo, int maxAmmo)
    {
        ammoText.text = $"Ammo: {ammo} / {maxAmmo}";
    }
    
    public void SetupCrosshair(WeaponConfig config)
    {
        if (crosshairDot != null)
        {
            crosshairDot.sprite = config.CrosshairDotSprite;
            crosshairDot.enabled = config.CrosshairDotSprite != null;
        }

        if (crosshairOuter != null && _outerRect != null)
        {
            crosshairOuter.sprite = config.CrosshairOuterSprite;
            crosshairOuter.enabled = config.CrosshairOuterSprite != null;
            
            _shrinkSpeed = config.CrosshairShrinkSpeed;
            _targetOuterSize = config.OuterDefaultSize;
            _outerRect.sizeDelta = config.OuterDefaultSize;
        }
    }
    
    public void SetAimCrosshair(bool isAiming, WeaponConfig config)
    {
        _targetOuterSize = isAiming ? config.OuterAimSize : config.OuterDefaultSize;
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
