using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObject/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement Settings")]
    public float MoveSpeed = 5.0f;
    public float RunSpeed = 8.0f;
    public float AimSpeed = 2.0f;
    
    [Header("Rotation Settings")]
    public float RotationSpeed = 2.0f;

    [Header("Jump Settings")] 
    public float JumpForce = 10.0f;
    
    [Header("Gravity Settings")]
    public float Gravity = 19f;
    
    [Header("Slope Settings")]
    public float SlopeAngle = 50.0f;
}
