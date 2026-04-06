using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class PathToDir : MonoBehaviour
{
    private Seeker seeker;
    private Transform self;

    [Header("Path")]
    [SerializeField] private float repathRate = 0.5f;
    [SerializeField] private float nextWaypointDistance = 0.6f;
    [SerializeField] private float reachedDistance = 0.4f;

    [Header("Direction")]
    [SerializeField] private float directionSmoothSpeed = 12f;

    [Header("Clamp")]
    [SerializeField] private bool clampToNearestNode = true;

    [Header("Debug")]
    [SerializeField] private bool showDebug = true;
    [SerializeField] private bool showGizmos = true;

    private Path currentPath;
    private int currentWaypointIndex;

    private Vector2 destination;
    private bool hasDestination;
    private float repathTimer;

    private Vector2 smoothDirection;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        self = transform;
    }

    private void Update()
    {
        UpdatePath();
    }

    public void SetDestination(Vector2 newDestination)
    {
        destination = ClampDestination(newDestination);
        hasDestination = true;
    }

    public void ClearDestination()
    {
        hasDestination = false;
        currentPath = null;
        currentWaypointIndex = 0;
        smoothDirection = Vector2.zero;
    }

    public Vector2 GetDirection()
    {
        Vector2 rawDirection = CalculateDirection();

        smoothDirection = Vector2.Lerp(
            smoothDirection,
            rawDirection,
            directionSmoothSpeed * Time.deltaTime
        );

        if (smoothDirection.sqrMagnitude <= 0.0001f)
            return Vector2.zero;

        return smoothDirection.normalized;
    }

    public bool ReachedDestination()
    {
        if (!hasDestination)
            return true;

        if (currentPath == null || currentPath.vectorPath == null || currentPath.vectorPath.Count == 0)
            return true;

        Vector2 endPoint = currentPath.vectorPath[currentPath.vectorPath.Count - 1];
        return Vector2.Distance(self.position, endPoint) <= reachedDistance;
    }

    private void UpdatePath()
    {
        if (!hasDestination)
            return;

        if (!seeker.IsDone())
            return;

        repathTimer -= Time.deltaTime;
        if (repathTimer > 0f)
            return;

        repathTimer = repathRate;
        seeker.StartPath(self.position, destination, OnPathComplete);
    }

    private Vector2 ClampDestination(Vector2 target)
    {
        if (!clampToNearestNode || AstarPath.active == null)
            return target;

        NNInfo nearest = AstarPath.active.GetNearest(target);

        if (nearest.node == null)
            return target;

        Vector2 clamped = (Vector3)nearest.position;

        if (showDebug && Vector2.Distance(target, clamped) > 0.05f)
        {
            Debug.Log($"[PathToDir] Clamp {target} -> {clamped}", this);
        }

        return clamped;
    }

    private Vector2 CalculateDirection()
    {
        if (currentPath == null || currentPath.vectorPath == null)
            return Vector2.zero;

        if (currentPath.vectorPath.Count <= 1)
            return Vector2.zero;

        Vector2 currentPos = self.position;
        Vector2 endPoint = currentPath.vectorPath[currentPath.vectorPath.Count - 1];

        if (Vector2.Distance(currentPos, endPoint) <= reachedDistance)
            return Vector2.zero;

        while (currentWaypointIndex < currentPath.vectorPath.Count - 1)
        {
            Vector2 waypoint = currentPath.vectorPath[currentWaypointIndex];

            if (Vector2.Distance(currentPos, waypoint) > nextWaypointDistance)
                break;

            currentWaypointIndex++;
        }

        if (currentWaypointIndex >= currentPath.vectorPath.Count)
            return Vector2.zero;

        Vector2 targetPoint = currentPath.vectorPath[currentWaypointIndex];
        Vector2 dir = targetPoint - currentPos;

        if (dir.sqrMagnitude <= 0.0001f)
            return Vector2.zero;

        return dir.normalized;
    }

    private void OnPathComplete(Path path)
    {
        if (path == null)
            return;

        if (path.error)
        {
            Debug.LogWarning("[PathToDir] Path error: " + path.errorLog, this);
            currentPath = null;
            currentWaypointIndex = 0;
            return;
        }

        currentPath = path;

        if (currentPath.vectorPath == null || currentPath.vectorPath.Count == 0)
        {
            currentWaypointIndex = 0;
            return;
        }

        currentWaypointIndex = FindClosestWaypointIndex();

        if (showDebug)
        {
            Vector2 endPoint = currentPath.vectorPath[currentPath.vectorPath.Count - 1];
            Debug.Log($"[PathToDir] Destination: {destination}, PathEnd: {endPoint}", this);
        }
    }

    private int FindClosestWaypointIndex()
    {
        int bestIndex = 0;
        float bestDistance = float.MaxValue;
        Vector2 currentPos = self.position;

        for (int i = 0; i < currentPath.vectorPath.Count; i++)
        {
            float dist = ((Vector2)currentPath.vectorPath[i] - currentPos).sqrMagnitude;
            if (dist < bestDistance)
            {
                bestDistance = dist;
                bestIndex = i;
            }
        }

        return bestIndex;
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(destination, 0.15f);

        if (currentPath != null && currentPath.vectorPath != null)
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < currentPath.vectorPath.Count - 1; i++)
            {
                Gizmos.DrawLine(currentPath.vectorPath[i], currentPath.vectorPath[i + 1]);
            }
        }
    }
}