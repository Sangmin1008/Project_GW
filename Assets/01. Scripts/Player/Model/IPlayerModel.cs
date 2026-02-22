using UnityEngine;
using UniRx;

public interface IPlayerModel
{
    IReadOnlyReactiveProperty<PlayerStateType> CurrentState { get; }
    IReadOnlyReactiveProperty<Vector3> CurrentVelocity { get; }
    IReadOnlyReactiveProperty<Vector2> CurrentLookAngle { get; }
    IReadOnlyReactiveProperty<string> CurrentAnimation { get; }
    
    void SetMoveInput(Vector2 input);
    public void SetRunInput(bool isRunning);
    public void SetJumpInput(bool isJumping);
    public void SetGrounded(bool isGrounded);
    public void ApplyGravity(float deltaTime);
    void Look(Vector2 input);
}
