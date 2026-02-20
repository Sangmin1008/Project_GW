using UnityEngine;

public class StateMachine
{
    public State CurrentState { get; private set; }
    public bool CanChangeState { get; private set; }

    public void Initialize(State state)
    {
        CanChangeState = true;
        CurrentState = state;
        CurrentState.Enter();
    }
    
    public void ChangeState(State state)
    {
        if (!CanChangeState)
            return;
        
        CurrentState.Exit();
        CurrentState = state;
        CurrentState.Enter();
    }
    
    public void SwitchOffStateMachine() => CanChangeState = false;
}
