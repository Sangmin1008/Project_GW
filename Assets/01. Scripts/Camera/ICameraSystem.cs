using UnityEngine;

public interface ICameraSystem
{
    void PlayShake(CameraShakeType type, float intensityMultiplier = 1f);
    void SetAimState(bool isAiming, float targetFOV = 40f);
}
