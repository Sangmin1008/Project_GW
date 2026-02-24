using System;
using UniRx;
using UnityEngine;

public abstract class BaseWeaponModel : IWeaponModel
{
    private float _lastFireTime;
    private float _lastReloadTime;
    
    protected readonly WeaponConfig _config;
    protected readonly ReactiveProperty<int> _currentAmmo;
    protected readonly ReactiveProperty<int> _reserveAmmo;
    protected readonly Subject<WeaponConfig> _onFired = new Subject<WeaponConfig>();
    
    public WeaponConfig Config => _config;
    public IReadOnlyReactiveProperty<int> CurrentAmmo => _currentAmmo;
    public IReadOnlyReactiveProperty<int> ReserveAmmo => _reserveAmmo;
    public IObservable<WeaponConfig> OnFired => _onFired;

    public BaseWeaponModel(WeaponConfig config)
    {
        _config = config;
        _currentAmmo = new ReactiveProperty<int>(config.MagCapacity);
        _reserveAmmo = new ReactiveProperty<int>(config.MaxAmmo);
    }

    public void TryFire()
    {
        if (_currentAmmo.Value > 0 && Time.time - _lastFireTime >= _config.FireRate)
        {
            _currentAmmo.Value--;
            _lastFireTime = Time.time;
            
            _onFired.OnNext(_config);
            
            Fire();
        }
    }

    public abstract void Fire();

    public void TryReload()
    {
        if (_currentAmmo.Value >= _config.MagCapacity || _reserveAmmo.Value <= 0)
            return;
        
        if (Time.time - _lastReloadTime < _config.ReloadRate)
            return;

        int neededAmmo = _config.MagCapacity - _currentAmmo.Value;
        int ammoToReload = Mathf.Min(neededAmmo, _reserveAmmo.Value);

        _currentAmmo.Value += ammoToReload;
        _reserveAmmo.Value -= ammoToReload;
        _lastReloadTime = Time.time;
    }
}