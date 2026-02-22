using UniRx;
using UnityEngine;

public class JumpState : State
{
    public JumpState(PlayerModel model, StateMachine stateMachine, string animationName) : base(model, stateMachine, animationName)
    {
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        
        _model.SetVerticalVelocity(_model.Config.JumpForce);
        
        _model.MoveInput
            .Subscribe(_ => _model.CalculateVelocity(_model.Config.MoveSpeed))
            .AddTo(StateDisposables);

        _model.CurrentVelocity
            .Where(v => v.y <= 0)
            .Subscribe(_ => _stateMachine.ChangeState(_model.FallState))
            .AddTo(StateDisposables);
    }
}
