using UniRx;

public class WeaponModel : IWeaponModel
{
    private readonly ReactiveProperty<int> _currentAmmo = new ReactiveProperty<int>(30);
    public IReadOnlyReactiveProperty<int> CurrentAmmo => _currentAmmo;
    
    public void Fire()
    {
        if (_currentAmmo.Value > 0)
        {
            _currentAmmo.Value--;
        }
    }
}