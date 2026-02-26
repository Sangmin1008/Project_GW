using System;
using UniRx;
using UnityEngine;
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
            .Subscribe(input => _model.SetMoveInput(input))
            .AddTo(_disposables);
        
        _view.OnRunInput
            .Subscribe(isRun => _model.SetRunInput(isRun))
            .AddTo(_disposables);
        
        _view.OnJumpInput
            .Subscribe(isJump => _model.SetJumpInput(isJump))
            .AddTo(_disposables);
        
        _view.OnGroundedState
            .Subscribe(isGrounded => _model.SetGrounded(isGrounded))
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
        
        _model.CurrentAnimation
            .DistinctUntilChanged()
            .Subscribe(animName => _view.PlayAnimation(animName))
            .AddTo(_disposables);
        
        Observable.EveryUpdate()
            .Subscribe(_ => _model.ApplyGravity(Time.deltaTime))
            .AddTo(_disposables);

        _view.OnGroundedState
            .Where(g => !g)
            .Subscribe(_ => _model.CaptureSpeed(_model.IsRunning.Value ? _model.Config.RunSpeed : _model.Config.MoveSpeed))
            .AddTo(_disposables);
        
        _view.OnSlopeDirection
            .Subscribe(slopeDir => _model.SetSlopeDirection(slopeDir))
            .AddTo(_disposables);
        
        _view.OnGroundNormal
            .Subscribe(normal => _model.SetGroundNormal(normal))
            .AddTo(_disposables);

    }

    public void Dispose() => _disposables.Dispose();
}
