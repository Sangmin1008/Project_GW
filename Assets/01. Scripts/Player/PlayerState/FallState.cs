using UnityEngine;

public class FallState : State
{
    public FallState(PlayerModel model, StateMachine stateMachine, string animationName) : base(model, stateMachine, animationName)
    {
    }

    protected override void OnEnter()
    {
        
    }
}
