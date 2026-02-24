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
}
