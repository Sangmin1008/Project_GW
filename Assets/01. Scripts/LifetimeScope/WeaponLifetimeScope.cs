using UnityEngine;
using VContainer;
using VContainer.Unity;

public class WeaponLifetimeScope : LifetimeScope
{
    [SerializeField] private WeaponView weaponView;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IWeaponModel, WeaponModel>(Lifetime.Singleton);
        builder.RegisterComponent(weaponView);
        builder.RegisterEntryPoint<WeaponPresenter>();
    }
}
