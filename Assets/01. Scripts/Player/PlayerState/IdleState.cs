using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerModel model, StateMachine stateMachine, string animationName) : base(model, stateMachine, animationName)
    {
    }

    protected override void OnEnter()
    {
        
    }
}
