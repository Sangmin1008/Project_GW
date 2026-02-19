using System;
using UniRx;
using VContainer.Unity;

public class PlayerPresenter : IStartable, IDisposable
{
    private readonly IPlayerModel _model;
    private readonly PlayerView _view;
    private readonly CompositeDisposable _disposables = new();
    
    public PlayerPresenter(IPlayerModel model, PlayerView view)
    {
        _model = model;
        _view = view;
    }
    
    public void Start()
    {
        _view.OnMoveInput
            .Subscribe(input => _model.Move(input))
            .AddTo(_disposables);
        
        _view.OnLookInput
            .Subscribe(input => _model.Look(input))
            .AddTo(_disposables);
        
        _model.CurrentVelocity
            .Subscribe(input => _view.ApplyVelocity(input))
            .AddTo(_disposables);
        
        _model.CurrentLookAngle
            .Subscribe(input => _view.ApplyLook(input))
            .AddTo(_disposables);
    }

    public void Dispose() => _disposables.Dispose();
}
