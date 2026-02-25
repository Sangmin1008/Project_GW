using UnityEngine;
using UniRx;

public interface IPlayerModel
{
    PlayerConfig Config { get; }
    StateMachine PlayerStateMachine { get; }
    IReadOnlyReactiveProperty<PlayerStateType> CurrentState { get; }
    IReadOnlyReactiveProperty<Vector3> CurrentVelocity { get; }
    IReadOnlyReactiveProperty<Vector2> CurrentLookAngle { get; }
    IReadOnlyReactiveProperty<string> CurrentAnimation { get; }
    IReadOnlyReactiveProperty<float> CapturedSpeed { get; }
    ReactiveProperty<Vector2> MoveInput { get; }
    ReactiveProperty<bool> IsRunning { get; }
    ReactiveProperty<bool> IsJumping { get; }
    ReactiveProperty<bool> IsGrounded { get; }

    
    void SetMoveInput(Vector2 input);
    public void SetRunInput(bool isRunning);
    public void SetJumpInput(bool isJumping);
    public void SetGrounded(bool isGrounded);
    public void ApplyGravity(float deltaTime);
    public void CaptureSpeed(float speed);
    public void ApplyRecoil(float recoilPitch, float recoilYaw);
    public void SetGroundNormal(Vector3 normal);
    void Look(Vector2 input);
}
