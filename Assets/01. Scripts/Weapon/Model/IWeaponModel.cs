using UniRx;

public interface IWeaponModel
{
    IReadOnlyReactiveProperty<int> CurrentAmmo { get; }
    void Fire();
}