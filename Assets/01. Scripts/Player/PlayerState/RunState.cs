using UnityEngine;

public class RunState : State
{
    public RunState(PlayerModel model, StateMachine stateMachine, string animationName) : base(model, stateMachine, animationName)
    {
    }

    protected override void OnEnter()
    {
        
    }
}
