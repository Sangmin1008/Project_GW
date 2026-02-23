using System;
using UniRx;

public interface IWeaponModel
{
    WeaponConfig Config { get; }
    IReadOnlyReactiveProperty<int> CurrentAmmo { get; }
    IObservable<WeaponConfig> OnFired { get; }
    void TryFire();
}