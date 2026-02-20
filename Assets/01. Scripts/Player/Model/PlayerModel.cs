using UniRx;
using UnityEngine;

public class PlayerModel : IPlayerModel
{
    public StateMachine PlayerStateMachine { get; private set; }
    public State IdleState { get; private set; }
    public State MoveState { get; private set; }
    
    private readonly ReactiveProperty<PlayerStateType> _currentState = new(PlayerStateType.Idle);
    public IReadOnlyReactiveProperty<PlayerStateType> CurrentState => _currentState;

    private readonly ReactiveProperty<Vector3> _currentVelocity = new(Vector3.zero);
    public IReadOnlyReactiveProperty<Vector3> CurrentVelocity => _currentVelocity;
    
    private readonly ReactiveProperty<Vector2> _currentLookAngle = new ReactiveProperty<Vector2>(Vector2.zero);
    public IReadOnlyReactiveProperty<Vector2> CurrentLookAngle => _currentLookAngle;
    
    private readonly ReactiveProperty<string> _currentAnimation = new("Idle");
    public IReadOnlyReactiveProperty<string> CurrentAnimation => _currentAnimation;
    
    
    private float _pitch = 0f;
    private float _yaw = 0f;

    public PlayerModel()
    {
        PlayerStateMachine = new StateMachine();

        // IdleState = new IdleState(this, PlayerStateMachine, "Idle");
        // MoveState = new MoveState(this, PlayerStateMachine, "Move");
        
        PlayerStateMachine.Initialize(IdleState);
    }

    
    public void Move(Vector2 input)
    {
        if (input.sqrMagnitude > 0.01f)
            _currentVelocity.Value = new Vector3(input.x, 0, input.y) * 3f;
        else
            _currentVelocity.Value = Vector3.zero;
    }

    public void Look(Vector2 input)
    {
        _yaw += input.x * 0.1f;
        _pitch -= input.y * 0.1f;
        _pitch = Mathf.Clamp(_pitch, -89f, 89f);

        _currentLookAngle.Value = new Vector2(_pitch, _yaw);
    }
    
    public void SetCurrentAnimation(string animName)
    {
        _currentAnimation.Value = animName;
    }
}
