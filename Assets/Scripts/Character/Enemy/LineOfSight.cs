using UnityEngine;

public class LineOfSight2D : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private bool showGizmos = true;

    [Header("Detect Range")]
    [SerializeField] private float detectRange = 10f;

    [Header("Field Of View")]
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private bool enableFOV = true;

    [Header("Raycast")]
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private bool enableRaycast = true;

    [Header("Direction")]
    [SerializeField] private Transform viewOrigin;
    [SerializeField] private bool useRightAsForward = true;

    public Transform Target => target;

    private void Awake()
    {
        if (viewOrigin == null)
            viewOrigin = transform;
    }

    private void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogError("Don't have Player in Scene");
            }
        }
    }

    public bool DetectRange()
    {
        if (target == null) return false;

        float sqrDistance = ((Vector2)(target.position - viewOrigin.position)).sqrMagnitude;
        return sqrDistance <= detectRange * detectRange;
    }

    public bool FOVAngle()
    {
        if (target == null) return false;

        Vector2 dirToTarget = ((Vector2)(target.position - viewOrigin.position)).normalized;
        Vector2 forward = GetForward2D();

        float angle = Vector2.Angle(forward, dirToTarget);
        return angle <= viewAngle * 0.5f;
    }

    public bool RaycastCheck()
    {
        if (target == null) return false;

        Vector2 origin = viewOrigin.position;
        Vector2 dirToTarget = ((Vector2)(target.position - viewOrigin.position)).normalized;
        float distance = Vector2.Distance(viewOrigin.position, target.position);

        RaycastHit2D hit = Physics2D.Raycast(origin, dirToTarget, distance, obstacleLayer);

        return hit.collider == null;
    }

    public bool CanSeeTarget()
    {
        if (target == null) return false;
        if (!DetectRange()) return false;
        if (enableFOV && !FOVAngle()) return false;
        if (enableRaycast && !RaycastCheck()) return false;

        return true;
    }

    private Vector2 GetForward2D()
    {
        return viewOrigin.lossyScale.x < 0 ? Vector2.left : Vector2.right;
    }

    private Vector2 DirFromAngle2D(float angleDeg)
    {
        float rad = angleDeg * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    private void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        Transform origin = viewOrigin != null ? viewOrigin : transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(origin.position, detectRange);

        // ✅ ใช้ forward ตัวเดียวกับ logic จริง
        Vector2 forward = GetForward2D();
        float forwardAngle = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;

        Vector2 leftDir = DirFromAngle2D(forwardAngle - viewAngle * 0.5f);
        Vector2 rightDir = DirFromAngle2D(forwardAngle + viewAngle * 0.5f);

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(origin.position, (Vector3)(leftDir * detectRange));
        Gizmos.DrawRay(origin.position, (Vector3)(rightDir * detectRange));

        if (target != null)
        {
            Gizmos.color = CanSeeTarget() ? Color.green : Color.red;
            Gizmos.DrawLine(origin.position, target.position);
        }
    }
}