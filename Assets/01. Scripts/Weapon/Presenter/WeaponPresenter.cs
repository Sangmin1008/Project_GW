using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer.Unity;

public class WeaponPresenter : IStartable, IDisposable
{
    private readonly IWeaponManagerModel _manager;
    private readonly WeaponView _view;
    private readonly ICameraSystem _cameraSystem;
    private readonly IPlayerModel _playerModel;
    
    private readonly CompositeDisposable _globalDisposables = new CompositeDisposable();
    private readonly SerialDisposable _weaponDisposable = new SerialDisposable();
    
    public WeaponPresenter(IWeaponManagerModel manager, WeaponView view, ICameraSystem cameraSystem, IPlayerModel playerModel)
    {
        _manager = manager;
        _view = view;
        _cameraSystem = cameraSystem;
        _playerModel = playerModel;
    }
    
    public void Start()
    {
        Bind();
    }

    private void Bind()
    {
        _view.OnFireRequested
            .Where(_ => _manager.CurrentWeapon.Value != null)
            .Subscribe(_ => _manager.CurrentWeapon.Value.TryFire())
            .AddTo(_globalDisposables);

        _view.OnReloadRequested
            .Subscribe(_ => _manager.CurrentWeapon.Value.TryReload())
            .AddTo(_globalDisposables);
        
        // _view.OnWeaponSwapInput
        //     .Subscribe(index => _manager.SwapWeapon(index))
        //     .AddTo(_globalDisposables);
        
        _manager.CurrentWeapon
            .Where(weapon => weapon != null)
            .Subscribe(weapon => BindCurrentWeapon(weapon))
            .AddTo(_globalDisposables);
        
        _view.OnAimInput
            .Subscribe(isAiming => 
            {
                if (_manager.CurrentWeapon.Value != null)
                {
                    WeaponConfig config = _manager.CurrentWeapon.Value.Config;
                    float aimFov = config.AimFOV;
                    
                    _cameraSystem.SetAimState(isAiming, aimFov);
                    _view.SetAimCrosshair(isAiming, config);
                }
                _playerModel.SetAimState(isAiming);
            })
            .AddTo(_globalDisposables);
    }

    private void BindCurrentWeapon(IWeaponModel currentWeapon)
    {
        var newWeaponDisposables = new CompositeDisposable();
        
        _view.SetWeaponName(currentWeapon.Config.WeaponName);
        _view.SetupCrosshair(currentWeapon.Config);

        Observable.CombineLatest(
                currentWeapon.CurrentAmmo, 
                currentWeapon.ReserveAmmo, 
                (current, reserve) => new { current, reserve }
            )
            .Subscribe(ammoData => 
            {
                _view.UpdateAmmoUI(ammoData.current, ammoData.reserve);
            })
            .AddTo(newWeaponDisposables);

        currentWeapon.OnFired
            .Subscribe(config =>
            {
                _view.PerformHitscan(config);
                _cameraSystem.PlayShake(config.ShakeType);
                float randomYaw = UnityEngine.Random.Range(-config.RecoilYaw, config.RecoilYaw);
                _playerModel.ApplyRecoil(config.RecoilPitch, randomYaw);
            })
            .AddTo(newWeaponDisposables);

        _weaponDisposable.Disposable = newWeaponDisposables;
    }

    public void Dispose()
    {
        _globalDisposables.Dispose();
        _weaponDisposable.Dispose();
    }}
