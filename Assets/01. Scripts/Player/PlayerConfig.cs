using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "ScriptableObject/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [Header("Movement Settings")]
    public float MoveSpeed = 5.0f;
    public float AimSpeed = 2.0f;
    
    [Header("Rotation Settings")]
    public float RotationSpeed = 2.0f;
}
