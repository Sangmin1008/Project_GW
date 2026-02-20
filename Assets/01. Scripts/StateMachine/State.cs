using UniRx;
using UnityEngine;

public abstract class State
{
    protected PlayerModel _model; 
    protected StateMachine _stateMachine;
    private string _animationName;
    
    protected CompositeDisposable StateDisposables = new CompositeDisposable();

    public State(PlayerModel model, StateMachine stateMachine, string animationName)
    {
        _model = model;
        _stateMachine = stateMachine;
        _animationName = animationName;
    }

    public virtual void Enter()
    {
        StateDisposables.Clear();
        _model.SetCurrentAnimation(_animationName); 
        
        OnEnter();
    }

    public virtual void Exit()
    {
        OnExit();
        StateDisposables.Clear();
    }

    protected abstract void OnEnter();
    protected virtual void OnExit() {}
}
