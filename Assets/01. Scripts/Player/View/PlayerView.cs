using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerView : MonoBehaviour
{
    public PlayerInput Input;
    
    private Camera _camera;
    private Vector3 _velocity;
    public IObservable<Vector2> OnMoveInput => Observable
        .EveryUpdate()
        .Select(_ => Input.Player.Move.ReadValue<Vector2>());
    public IObservable<Vector2> OnLookInput => Observable
        .EveryUpdate()
        .Select(_ => Input.Player.Look.ReadValue<Vector2>());

    void Awake()
    {
        Input = new PlayerInput();
        
        _camera = Camera.main;
    }
    
    void OnEnable() => Input.Enable();
    void OnDisable() => Input.Disable();

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        BindMovement();
    }


    public void ApplyVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    private void BindMovement()
    {
        this.FixedUpdateAsObservable()
            .Select(_ => _velocity)
            .Subscribe(input =>
            {
                if (input.sqrMagnitude > 0)
                {
                    transform.Translate(input * Time.fixedDeltaTime, Space.Self);
                }
            });
    }

    public void ApplyLook(Vector2 lookAngle)
    {
        float pitch = lookAngle.x;
        float yaw = lookAngle.y;
        
        _camera.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        transform.localRotation = Quaternion.Euler(0, yaw, 0);
    }
}
