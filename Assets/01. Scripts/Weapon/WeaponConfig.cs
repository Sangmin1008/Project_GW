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
    public CameraShakeType ShakeType;
    
    [Header("Recoil Settings (반동)")]
    public float HipRecoilYaw = 1.5f;
    public float HipRecoilPitch = 1.5f;
    public float AimRecoilYaw = 0.5f;
    public float AimRecoilPitch = 0.5f;
    
    [Header("Spread Settings (탄퍼짐)")]
    public float HipSpread = 0.05f;
    public float AimSpread = 0.01f;
    
    [Header("Crosshair Settings")]
    public Sprite CrosshairDotSprite;
    public Sprite CrosshairOuterSprite;
    public Vector2 OuterDefaultSize = new Vector2(100f, 100f);
    public Vector2 OuterAimSize = new Vector2(40f, 40f);
    public float CrosshairShrinkSpeed = 15f;
    
    [Header("Ballistics")]
    public float BulletSpeed = 800f;

    [Header("Impact Effects")]
    public GameObject ImpactParticlePrefab;
    public GameObject BulletHoleDecalPrefab;
}
