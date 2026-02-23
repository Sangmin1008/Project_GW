using UnityEngine;
using VContainer;
using VContainer.Unity;

public class WeaponLifetimeScope : LifetimeScope
{
    [Header("Weapon Settings")]
    [SerializeField] private WeaponView weaponView;
    [SerializeField] private WeaponConfig weaponConfig;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(weaponConfig);
        builder.Register<IWeaponModel, BaseWeaponModel>(Lifetime.Singleton);
        builder.RegisterComponent(weaponView);
        builder.RegisterEntryPoint<WeaponPresenter>();
    }
}
