using UniRx;
using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerModel model, StateMachine stateMachine, string animationName) : base(model, stateMachine, animationName)
    {
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        _model.CalculateVelocity(0f);

        _model.IsGrounded
            .Where(g => !g)
            .Subscribe(_ => _stateMachine.ChangeState(_model.FallState))
            .AddTo(StateDisposables);
        
        _model.IsJumping
            .Where(j => j)
            .Subscribe(_ => _stateMachine.ChangeState(_model.JumpState))
            .AddTo(StateDisposables);
        
        _model.MoveInput
            .Where(input => input.sqrMagnitude > 0)
            .Subscribe(_ => 
            {
                _stateMachine.ChangeState(_model.IsRunning.Value ? _model.RunState : _model.MoveState);
            })
            .AddTo(StateDisposables);
    }
}
