using UnityEngine;

public class JumpState : State
{
    public JumpState(PlayerModel model, StateMachine stateMachine, string animationName) : base(model, stateMachine, animationName)
    {
    }

    protected override void OnEnter()
    {
        
    }
}
