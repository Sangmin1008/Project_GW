using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerLifetimeScope : LifetimeScope
{
    [SerializeField] private PlayerView playerView;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IPlayerModel, PlayerModel>(Lifetime.Singleton);
        builder.RegisterComponent(playerView);
        builder.RegisterEntryPoint<PlayerPresenter>();
    }
}
