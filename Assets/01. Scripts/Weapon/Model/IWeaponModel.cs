using System;
using UniRx;

public interface IWeaponModel
{
    IReadOnlyReactiveProperty<int> CurrentAmmo { get; }
    IObservable<WeaponConfig> OnFired { get; }
    void TryFire();
}