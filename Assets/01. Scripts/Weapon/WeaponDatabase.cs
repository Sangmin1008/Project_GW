using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "ScriptableObject/WeaponDatabase")]
public class WeaponDatabase : ScriptableObject
{
    public List<WeaponConfig> InitialWeapons;
}
