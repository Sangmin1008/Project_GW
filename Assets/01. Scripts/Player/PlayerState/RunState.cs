using UniRx;
using UnityEngine;

public class RunState : State
{
    public RunState(PlayerModel model, StateMachine stateMachine, string animationName) : base(model, stateMachine, animationName)
    {
    }

    protected override void OnEnter()
    {
        base.OnEnter();

        _model.MoveInput
            .Subscribe(input =>
            {
                if (input.sqrMagnitude == 0) _stateMachine.ChangeState(_model.IdleState);
                else _model.CalculateVelocity(_model.Config.RunSpeed);
            })
            .AddTo(StateDisposables);

        _model.IsRunning
            .Where(r => !r)
            .Subscribe(_ => _stateMachine.ChangeState(_model.MoveState))
            .AddTo(StateDisposables);
        
        _model.IsJumping
            .Where(j => j && _model.IsGrounded.Value)
            .Subscribe(_ => _stateMachine.ChangeState(_model.JumpState))
            .AddTo(StateDisposables);
        
        _model.IsGrounded
            .Where(g => !g)
            .Subscribe(_ => _stateMachine.ChangeState(_model.FallState))
            .AddTo(StateDisposables);
        
        Observable.EveryUpdate()
            .Subscribe(_ => _model.CalculateVelocity(_model.Config.RunSpeed))
            .AddTo(StateDisposables);
    }
}
