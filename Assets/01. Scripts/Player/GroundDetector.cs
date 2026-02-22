using System;
using UniRx;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private readonly ReactiveProperty<bool> _isGrounded = new(false);
    public IObservable<bool> OnGroundedChanged => _isGrounded.AsObservable();

    [field: SerializeField] public bool IsGrounded { get; private set; }

    [Header("Ground Detection")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    
    [Header("Direction Info")]
    public Vector3 GroundNormal { get; private set; } = Vector3.up;
    public Vector3 SlopeDirection { get; private set; } = Vector3.forward;

    private struct GroundHitResult
    {
        public bool HasHit;
        public RaycastHit HitInfo;
    }

    void Start()
    {
        Observable.EveryFixedUpdate()
            .Select(_ => 
            {
                bool hit = Physics.Raycast(groundCheck.position, Vector3.down, out RaycastHit hitInfo, groundCheckDistance, groundLayer);
                return new GroundHitResult { HasHit = hit, HitInfo = hitInfo };
            })
            .Do(result => CalculateGroundVectors(result.HasHit, result.HitInfo))
            .Select(result => result.HasHit)
            .DistinctUntilChanged()
            .ThrottleFrame(3)
            .Subscribe(isGrounded => 
            {
                IsGrounded = isGrounded;
                _isGrounded.Value = isGrounded;
            })
            .AddTo(this);
    }

    private void CalculateGroundVectors(bool hasHit, RaycastHit hit)
    {
        if (hasHit)
        {
            GroundNormal = hit.normal;
            
            Vector3 slopeRight = Vector3.Cross(Vector3.up, GroundNormal);
            SlopeDirection = Vector3.Cross(GroundNormal, slopeRight).normalized;
        }
        else
        {
            GroundNormal = Vector3.up;
            SlopeDirection = Vector3.forward;
        }
    }
    
    private void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = IsGrounded ? Color.green : Color.red;

        Vector3 startPos = groundCheck.position;
        Vector3 endPos = startPos + (Vector3.down * groundCheckDistance);

        Gizmos.DrawLine(startPos, endPos);
        
        if (IsGrounded)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(startPos, startPos + SlopeDirection);
        }
    }
}