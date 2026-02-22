using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerView playerView;
    [SerializeField] private PlayerConfig playerConfig;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(playerConfig);
        builder.Register<IPlayerModel, PlayerModel>(Lifetime.Singleton);
        builder.RegisterComponent(playerView);
        builder.RegisterEntryPoint<PlayerPresenter>();
    }
}
