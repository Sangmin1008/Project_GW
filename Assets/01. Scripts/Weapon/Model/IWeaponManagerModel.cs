using UniRx;

public interface IWeaponManagerModel
{
    IReadOnlyReactiveProperty<IWeaponModel> CurrentWeapon { get; }
    void SwapWeapon(int index);
}
