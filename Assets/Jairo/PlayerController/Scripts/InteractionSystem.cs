using FIMSpace.FLook;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] private Transform cameraRotTransform = default;
    [SerializeField] private LayerMask raycastLayer = default;
    [SerializeField] private float raycastRate = 0.25F;
    [SerializeField] private float detectionRadius = 0.25F;
    [SerializeField] private float detectionAngle = 140F;
    [SerializeField] private float maxRayDistance = 1.5F;
    [SerializeField] private float maxInteractionDistance = 1.0F;
    [SerializeField] private float lookAtSmooth = 8F;

    private float normalLookAmount;
    private float nextRaycast = 0;
    private FLookAnimator lookAnimator;
    private Transform originalLookAtTarget;
    private UIInteractionIndicator interactionIndicator;

    public InteractableObject Current { get; private set; }

    private float currentLookAt;

    private void Awake()
    {
        lookAnimator = GetComponent<FLookAnimator>();
        originalLookAtTarget = lookAnimator.ObjectToFollow;
        normalLookAmount = lookAnimator.LookAnimatorAmount;
        interactionIndicator = FindObjectOfType<UIInteractionIndicator>();
        interactionIndicator.Set(null);
    }

    private void Update()
    {
        if (Time.time > nextRaycast)
        {
            Current = null;
            if (Physics.SphereCast(cameraRotTransform.position, detectionRadius, cameraRotTransform.forward, out RaycastHit hit, maxRayDistance, raycastLayer))
            {
                Vector3 direction = (hit.point - cameraRotTransform.position);
                if (!Physics.Raycast(cameraRotTransform.position, direction, hit.distance))
                {
                    if (IsInDistance(hit.point) && IsTargetInAngle(hit.collider))
                    {
                        if(hit.collider.TryGetComponent(out InteractableObject interactableObject))
                            Current = interactableObject;
                        else
                            Debug.LogWarning($"Object {hit.collider.name} don't have a InteractableObject component.");
                    }
                }
            }
            nextRaycast = Time.time + raycastRate;
        }

        if (Current != interactionIndicator.Current)
            interactionIndicator.Set(Current);

        currentLookAt = Current == null ? normalLookAmount : 1.0F;
        lookAnimator.SetLookTarget(Current == null ? originalLookAtTarget : Current.transform);
        lookAnimator.LookAnimatorAmount = Mathf.Lerp(lookAnimator.LookAnimatorAmount, currentLookAt, Time.deltaTime * lookAtSmooth);
    }

    private bool IsInDistance(Vector3 point)
    {
        point.y = 0;
        Vector3 playerPos = transform.position;
        playerPos.y = 0;
        return Vector3.Distance(point, playerPos) <= maxInteractionDistance;
    }
    
    private bool IsTargetInAngle(Collider target)
    {
        Vector3 visionPos = lookAnimator.LeadBone.position;
        Vector3 targetPos = target.bounds.center;
        Vector3 direction = (targetPos - visionPos).normalized;
        Vector3 forward = lookAnimator.LeadBone.forward;
        float targetAngle = Vector3.Angle(forward, direction);
        float angleToCheck = detectionAngle / 2F;
        return targetAngle < angleToCheck;
    }
    
    private void OnDrawGizmos()
    {
        if (cameraRotTransform == null)
            return;
        Vector3 originPos = cameraRotTransform.position;
        Gizmos.DrawWireSphere(originPos, maxRayDistance);

        if (Physics.SphereCast(originPos, detectionRadius, cameraRotTransform.forward * maxRayDistance, out RaycastHit hit, maxRayDistance, raycastLayer))
        {
            Gizmos.color = Current != null ? Color.green : Color.yellow;
            Vector3 sphereCastMidpoint = originPos + (cameraRotTransform.forward * hit.distance);
            Gizmos.DrawWireSphere(sphereCastMidpoint, detectionRadius);
            Gizmos.DrawSphere(hit.point, 0.1f);
            Debug.DrawLine(originPos, hit.point, Gizmos.color);
        }
        else
        {
            Gizmos.color = Color.red;
            Vector3 sphereCastMidpoint = originPos + (cameraRotTransform.forward * (maxRayDistance-detectionRadius));
            Gizmos.DrawWireSphere(sphereCastMidpoint, detectionRadius);
            Debug.DrawLine(originPos, sphereCastMidpoint, Color.red);
        }

        if (lookAnimator == null || lookAnimator.LeadBone == null)
        {
            lookAnimator = GetComponent<FLookAnimator>();
            return;
        }
        
        Vector3 visionPos = lookAnimator.LeadBone.position;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(visionPos, visionPos + GetDirectionFromAngle(true, false) * maxRayDistance);
        Gizmos.DrawLine(visionPos, visionPos + GetDirectionFromAngle(false, false) * maxRayDistance);
        Gizmos.DrawLine(visionPos, visionPos + GetDirectionFromAngle(true, true) * maxRayDistance);
        Gizmos.DrawLine(visionPos, visionPos + GetDirectionFromAngle(false, true) * maxRayDistance);
    }
    
    private Vector3 GetDirectionFromAngle(bool isNegative, bool isVertical)
    {
        float ang = detectionAngle;
        float eulerY = lookAnimator.LeadBone.eulerAngles.y;
        float angleDeg = ((isNegative ? -ang : ang) / 2F) + eulerY;
        return isVertical ? new Vector3(Mathf.Sin(angleDeg * Mathf.Deg2Rad), 0, Mathf.Cos(angleDeg * Mathf.Deg2Rad)) 
            : new Vector3(0, Mathf.Sin(angleDeg * Mathf.Deg2Rad), Mathf.Cos(angleDeg * Mathf.Deg2Rad));
    }
}
