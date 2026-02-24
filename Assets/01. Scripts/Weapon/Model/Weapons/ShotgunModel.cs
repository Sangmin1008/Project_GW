using UnityEngine;

public class ShotgunModel : BaseWeaponModel
{
    public ShotgunModel(WeaponConfig config) : base(config)
    {
    }

    public override void Fire()
    {
        Debug.Log(Config.WeaponName + " Fired!");
    }
}
