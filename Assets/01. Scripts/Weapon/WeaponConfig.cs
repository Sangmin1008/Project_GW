using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig", menuName = "ScriptableObject/WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public string WeaponName = "NormalWeapon";
    public int MaxAmmo = 30;
    public float FireRate = 0.1f;
    public int Damage = 10;
    public float Range = 100f;
}
