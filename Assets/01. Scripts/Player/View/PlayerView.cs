using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Rendering;
using VContainer;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(GroundDetector))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform cameraRoot;
    
    public PlayerInput Input;
    
    private Vector3 _velocity;
    private CharacterController _characterController;
    private GroundDetector _groundDetector;
    private Animator _animator;
    
    public IObservable<Vector2> OnMoveInput => Observable
        .EveryUpdate()
        .Select(_ => Input.Player.Move.ReadValue<Vector2>());
    public IObservable<Vector2> OnLookInput => Observable
        .EveryUpdate()
        .Select(_ => Input.Player.Look.ReadValue<Vector2>());
    public IObservable<bool> OnRunInput => Observable
        .EveryUpdate()
        .Select(_ => Input.Player.Run.IsPressed())
        .DistinctUntilChanged();
    public IObservable<bool> OnJumpInput => Observable
        .EveryUpdate()
        .Select(_ => Input.Player.Jump.IsPressed())
        .DistinctUntilChanged();
    public IObservable<bool> OnGroundedState => Observable
        .EveryUpdate()
        .Select(_ => _groundDetector.IsGrounded)
        .DistinctUntilChanged();
    
    [Inject]
    public void Construct(PlayerInput playerInput)
    {
        Input = playerInput;
    }

    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _groundDetector = GetComponent<GroundDetector>();
        _animator = GetComponentInChildren<Animator>();
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
                _characterController.Move(input * Time.fixedDeltaTime);
            });
    }

    public void ApplyLook(Vector2 lookAngle)
    {
        float pitch = lookAngle.x;
        float yaw = lookAngle.y;
        
        cameraRoot.localRotation = Quaternion.Euler(pitch, 0, 0);
        transform.localRotation = Quaternion.Euler(0, yaw, 0);
    }

    public void PlayAnimation(string name)
    {
        
    }
}
