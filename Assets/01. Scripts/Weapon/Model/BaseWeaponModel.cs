using System;
using UniRx;
using UnityEngine;

public abstract class BaseWeaponModel : IWeaponModel
{
    private float _lastFireTime;
    
    protected readonly WeaponConfig _config;
    protected readonly ReactiveProperty<int> _currentAmmo;
    protected readonly Subject<WeaponConfig> _onFired = new Subject<WeaponConfig>();
    
    public WeaponConfig Config => _config;
    public IReadOnlyReactiveProperty<int> CurrentAmmo => _currentAmmo;
    public IObservable<WeaponConfig> OnFired => _onFired;

    public BaseWeaponModel(WeaponConfig config)
    {
        _config = config;
        _currentAmmo = new ReactiveProperty<int>(config.MaxAmmo);
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
}