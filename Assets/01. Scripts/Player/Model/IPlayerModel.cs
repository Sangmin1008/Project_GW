using UnityEngine;
using UniRx;

public interface IPlayerModel
{
    IReadOnlyReactiveProperty<PlayerStateType> CurrentState { get; }
    IReadOnlyReactiveProperty<Vector3> CurrentVelocity { get; }
    IReadOnlyReactiveProperty<Vector2> CurrentLookAngle { get; }
    IReadOnlyReactiveProperty<string> CurrentAnimation { get; }
    
    void Move(Vector2 input);
    void Look(Vector2 input);
}
