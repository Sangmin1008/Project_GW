using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class WeaponManagerModel : IWeaponManagerModel
{
    private readonly List<IWeaponModel> _inventory = new List<IWeaponModel>();
    private readonly ReactiveProperty<IWeaponModel> _currentWeapon = new ReactiveProperty<IWeaponModel>();
    
    public IReadOnlyReactiveProperty<IWeaponModel> CurrentWeapon => _currentWeapon;

    public WeaponManagerModel(WeaponDatabase database)
    {
        foreach (var config in database.InitialWeapons)
        {
            if (config.WeaponName == "Shotgun") _inventory.Add(new ShotgunModel(config));
        }
        
        if (_inventory.Count > 0) SwapWeapon(0);
    }
    
    public void SwapWeapon(int index)
    {
        if (index >= 0 && index < _inventory.Count)
        {
            _currentWeapon.Value = _inventory[index];
        }
    }

}
