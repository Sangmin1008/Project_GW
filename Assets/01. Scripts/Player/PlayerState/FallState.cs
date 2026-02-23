using UniRx;
using UnityEngine;

public class FallState : State
{
    public FallState(PlayerModel model, StateMachine stateMachine, string animationName) : base(model, stateMachine, animationName)
    {
    }

    protected override void OnEnter()
    {
        base.OnEnter();

        _model.MoveInput
            .Subscribe(_ => _model.CalculateVelocity(_model.Config.MoveSpeed))
            .AddTo(StateDisposables);

        _model.IsGrounded
            .Where(g => g)
            .Subscribe(_ => 
            {
                if (_model.MoveInput.Value.sqrMagnitude == 0)
                    _stateMachine.ChangeState(_model.IdleState);
                else
                    _stateMachine.ChangeState(_model.IsRunning.Value ? _model.RunState : _model.MoveState);
            })
            .AddTo(StateDisposables);
    }
}
