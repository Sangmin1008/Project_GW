using UnityEngine;

public class MoveState : State
{
    public MoveState(PlayerModel model, StateMachine stateMachine, string animationName) : base(model, stateMachine, animationName)
    {
    }

    protected override void OnEnter()
    {
        
    }
}
