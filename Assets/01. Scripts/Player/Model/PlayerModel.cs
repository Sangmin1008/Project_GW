using UniRx;
using UnityEngine;

public class PlayerModel : IPlayerModel
{
    public PlayerConfig Config { get; private set; }
    public StateMachine PlayerStateMachine { get; private set; }
    public State IdleState { get; private set; }
    public State MoveState { get; private set; }
    public State RunState { get; private set; }
    public State JumpState { get; private set; }
    public State FallState { get; private set; }
    
    
    private readonly ReactiveProperty<PlayerStateType> _currentState = new(PlayerStateType.Idle);
    private readonly ReactiveProperty<Vector3> _currentVelocity = new(Vector3.zero);
    private readonly ReactiveProperty<Vector2> _currentLookAngle = new(Vector2.zero);
    private readonly ReactiveProperty<string> _currentAnimation = new("Idle");
    private readonly ReactiveProperty<float> _capturedSpeed = new(0f);

    public IReadOnlyReactiveProperty<PlayerStateType> CurrentState => _currentState;
    public IReadOnlyReactiveProperty<Vector3> CurrentVelocity => _currentVelocity;
    public IReadOnlyReactiveProperty<Vector2> CurrentLookAngle => _currentLookAngle;
    public IReadOnlyReactiveProperty<string> CurrentAnimation => _currentAnimation;
    public IReadOnlyReactiveProperty<float> CapturedSpeed => _capturedSpeed;

    public ReactiveProperty<Vector2> MoveInput { get; } = new(Vector2.zero);
    public ReactiveProperty<bool> IsRunning { get; } = new(false);
    public ReactiveProperty<bool> IsJumping { get; } = new(false);
    public ReactiveProperty<bool> IsGrounded { get; } = new(true);
    
    
    private float _pitch = 0f;
    private float _yaw = 0f;
    private Vector3 _groundNormal = Vector3.up;

    public PlayerModel(PlayerConfig config)
    {
        Config = config;
        PlayerStateMachine = new StateMachine();

        IdleState   = new IdleState(this, PlayerStateMachine, "Idle");
        MoveState   = new MoveState(this, PlayerStateMachine, "Move");
        RunState    = new RunState(this, PlayerStateMachine, "Run");
        JumpState   = new JumpState(this, PlayerStateMachine, "Jump");
        FallState   = new FallState(this, PlayerStateMachine, "Fall");
        
        PlayerStateMachine.Initialize(IdleState);
    }

    public void SetMoveInput(Vector2 input) => MoveInput.Value = input;
    public void SetRunInput(bool isRunning) => IsRunning.Value = isRunning;
    public void SetJumpInput(bool isJumping) => IsJumping.Value = isJumping;
    public void SetGrounded(bool isGrounded) => IsGrounded.Value = isGrounded;
    
    public void CalculateVelocity(float speed)
    {
        if (MoveInput.Value.sqrMagnitude > 0.01f)
        {
            Vector3 localDirection = new Vector3(MoveInput.Value.x, 0, MoveInput.Value.y);
            Quaternion playerRotation = Quaternion.Euler(0, _currentLookAngle.Value.y, 0);
            Vector3 worldDirection = playerRotation * localDirection;
            
            _currentVelocity.Value = new Vector3(worldDirection.x * speed, _currentVelocity.Value.y, worldDirection.z * speed);
        }
        else
        {
            _currentVelocity.Value = new Vector3(0, _currentVelocity.Value.y, 0);
        }
    }

    public void SetVerticalVelocity(float yVelocity)
    {
        _currentVelocity.Value = new Vector3(_currentVelocity.Value.x, yVelocity, _currentVelocity.Value.z);
    }

    public void Look(Vector2 input)
    {
        _yaw += input.x * Config.RotationSpeed;
        _pitch -= input.y * Config.RotationSpeed;
        _pitch = Mathf.Clamp(_pitch, -89f, 89f);

        _currentLookAngle.Value = new Vector2(_pitch, _yaw);
    }
    
    public void ApplyGravity(float deltaTime)
    {
        Vector3 currentVel = _currentVelocity.Value;

        if (IsGrounded.Value && currentVel.y < 0)
        {
            _currentVelocity.Value = new Vector3(currentVel.x, -2f, currentVel.z);
        }
        else
        {
            float newY = currentVel.y + Config.Gravity * deltaTime;
            _currentVelocity.Value = new Vector3(currentVel.x, newY, currentVel.z);
        }
    }
    
    public void SetCurrentAnimation(string animName)
    {
        _currentAnimation.Value = animName;
    }

    public void CaptureSpeed(float speed)
    {
        _capturedSpeed.Value = speed;
    }
    
    public void ApplyRecoil(float recoilPitch, float recoilYaw)
    {
        _pitch -= recoilPitch; 
    
        _yaw += recoilYaw;     
    
        _pitch = Mathf.Clamp(_pitch, -89f, 89f);
        _currentLookAngle.Value = new Vector2(_pitch, _yaw);
    }
    
    public void SetGroundNormal(Vector3 normal) => _groundNormal = normal;
}
