using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class WeaponView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Button fireButton;
    
    public IObservable<Unit> OnFireRequested => fireButton.OnClickAsObservable();

    public void UpdateAmmoUI(int ammo)
    {
        ammoText.text = $"Ammo: {ammo} / 30";
    }

    public void PlayFireEffect()
    {
        Debug.Log("fire!");
    }
}
