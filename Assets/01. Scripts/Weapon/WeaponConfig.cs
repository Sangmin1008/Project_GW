using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObject/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    [Header("Weapon Name")]
    public string WeaponName = "NormalWeapon";
    
    [Header("Settings")]
    public int MaxAmmo = 30;
    public int MagCapacity = 10;
    public float FireRate = 0.1f;
    public float ReloadRate = 3f;
    public int Damage = 10;
    public float Range = 100f;
    public float AimFOV = 40f;
    public float RecoilYaw = 1f;
    public float RecoilPitch = 1f;
    public CameraShakeType ShakeType;
    
    [Header("Crosshair Settings")]
    public Sprite CrosshairDotSprite;
    public Sprite CrosshairOuterSprite;
    public Vector2 OuterDefaultSize = new Vector2(100f, 100f);
    public Vector2 OuterAimSize = new Vector2(40f, 40f);
    public float CrosshairShrinkSpeed = 15f;
}
