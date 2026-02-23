using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer.Unity;

public class WeaponPresenter : IStartable, IDisposable
{
    private readonly IWeaponModel _model;
    private readonly WeaponView _view;
    private readonly WeaponConfig _config;
    private readonly CompositeDisposable _disposables = new CompositeDisposable();
    
    public WeaponPresenter(IWeaponModel model, WeaponView view, WeaponConfig config)
    {
        _model = model;
        _view = view;
        _config = config;
    }
    
    public void Start()
    {
        Bind();
    }

    private void Bind()
    {
        _view.OnFireRequested
            .Subscribe(_ => _model.TryFire())
            .AddTo(_disposables);

        _model.CurrentAmmo
            .Subscribe(ammo => _view.UpdateAmmoUI(ammo, _config.MaxAmmo))
            .AddTo(_disposables);

        _model.OnFired
            .Subscribe(config => _view.PerformHitscan(config))
            .AddTo(_disposables);
    }

    public void Dispose() => _disposables.Dispose();
}
