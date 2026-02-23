using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    [Header("Player Settings")]
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerConfig playerConfig;
    
    [Header("Weapon Settings")]
    [SerializeField] private WeaponView weaponView;
    [SerializeField] private WeaponDatabase weaponDatabase;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<PlayerInput>(Lifetime.Singleton);
        
        builder.RegisterInstance(playerConfig);
        builder.Register<IPlayerModel, PlayerModel>(Lifetime.Singleton);
        builder.RegisterComponent(playerView);
        builder.RegisterEntryPoint<PlayerPresenter>();
        
        builder.RegisterInstance(weaponDatabase);
        builder.Register<IWeaponManagerModel, WeaponManagerModel>(Lifetime.Singleton);
        builder.RegisterComponent(weaponView);
        builder.RegisterEntryPoint<WeaponPresenter>();
    }
}
