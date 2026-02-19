using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using VContainer.Unity;

public class WeaponPresenter : IAsyncStartable, IDisposable
{
    private readonly IWeaponModel _model;
    private readonly WeaponView _view;
    private readonly CompositeDisposable _disposables = new CompositeDisposable();
    
    public WeaponPresenter(IWeaponModel model, WeaponView view)
    {
        _model = model;
        _view = view;
    }
    
    public async UniTask StartAsync(CancellationToken cancellation)
    {
        Debug.Log("무기 시스템 리소스 로드 중...");
        
        await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: cancellation);
        
        Debug.Log("무기 시스템 초기화 완료! 바인딩 시작.");
        Bind();
    }

    private void Bind()
    {
        _model.CurrentAmmo
            .Subscribe(ammo => 
            {
                _view.UpdateAmmoUI(ammo);
                if (ammo < 30)
                {
                    _view.PlayFireEffect();
                }
            })
            .AddTo(_disposables);

        _view.OnFireRequested
            .Subscribe(_ => _model.Fire())
            .AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables.Dispose(); 
    }
}
