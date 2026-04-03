using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class PathToDir : MonoBehaviour
{
    private Seeker seeker;
    private Transform self;

    [Header("Path Settings")]
    [SerializeField] private float repathRate = 0.35f;
    [SerializeField] private float nextWaypointDistance = 0.4f;
    [SerializeField] private float reachedDestinationDistance = 0.4f;

    [Header("Direction Smoothing")]
    [SerializeField] private float directionSmoothSpeed = 12f;
    [SerializeField] private float keepDirectionGraceTime = 0.08f;

    private float defaultRepathRate;
    private float defaultNextWaypointDistance;
    private float defaultReachedDestinationDistance;

    private Path currentPath;
    private int currentWaypointIndex;
    private Vector2 currentDestination;
    private bool hasDestination;
    private float repathTimer;

    // smoothing state
    private Vector2 smoothedDirection;
    private Vector2 lastRawDirection;
    private float lastNonZeroDirectionTime;

    public bool HasDestination => hasDestination;
    public bool HasPath => currentPath != null && currentPath.vectorPath != null && currentPath.vectorPath.Count > 0;

    private void Awake()
    {
        seeker = GetComponent<Seeker>();
        self = transform;

        defaultRepathRate = repathRate;
        defaultNextWaypointDistance = nextWaypointDistance;
        defaultReachedDestinationDistance = reachedDestinationDistance;
    }

    private void Update()
    {
        TickPathUpdate();
    }

    public void SetDestination(Vector2 destination, bool forceRepath = false)
    {
        currentDestination = destination;
        hasDestination = true;

        if (forceRepath)
            repathTimer = 0f;
    }

    public void ClearDestination()
    {
        hasDestination = false;
        currentPath = null;
        currentWaypointIndex = 0;
        smoothedDirection = Vector2.zero;
        lastRawDirection = Vector2.zero;
    }

    private void TickPathUpdate()
    {
        if (!hasDestination)
            return;

        if (seeker == null)
            return;

        repathTimer -= Time.deltaTime;
        if (repathTimer > 0f)
            return;

        repathTimer = repathRate;

        if (!seeker.IsDone())
            return;

        seeker.StartPath(self.position, currentDestination, OnPathComplete);
    }

    public Vector2 GetDirection()
    {
        Vector2 rawDirection = CalculateRawDirection();

        // ถ้า raw เป็นศูนย์ชั่วคราวจากการสลับ path/waypoint
        // ให้ค้างทิศเดิมไว้แป๊บหนึ่ง ลดอาการสะดุด
        if (rawDirection != Vector2.zero)
        {
            lastRawDirection = rawDirection;
            lastNonZeroDirectionTime = Time.time;
        }
        else if (Time.time - lastNonZeroDirectionTime <= keepDirectionGraceTime)
        {
            rawDirection = lastRawDirection;
        }

        // smooth ภายใน PathToDir เลย
        smoothedDirection = Vector2.Lerp(
            smoothedDirection,
            rawDirection,
            directionSmoothSpeed * Time.deltaTime
        );

        if (smoothedDirection.sqrMagnitude <= 0.0001f)
            return Vector2.zero;

        return smoothedDirection.normalized;
    }

    private Vector2 CalculateRawDirection()
    {
        if (!HasPath)
            return Vector2.zero;

        if (currentWaypointIndex >= currentPath.vectorPath.Count)
            return Vector2.zero;

        Vector2 currentPos = self.position;
        Vector2 waypoint = currentPath.vectorPath[currentWaypointIndex];

        while (currentWaypointIndex < currentPath.vectorPath.Count - 1 &&
               Vector2.Distance(currentPos, waypoint) <= nextWaypointDistance)
        {
            currentWaypointIndex++;
            waypoint = currentPath.vectorPath[currentWaypointIndex];
        }

        int lookAheadIndex = Mathf.Min(currentWaypointIndex + 1, currentPath.vectorPath.Count - 1);
        Vector2 targetPoint = currentPath.vectorPath[lookAheadIndex];

        Vector2 dir = targetPoint - currentPos;

        if (dir.sqrMagnitude <= 0.0001f)
            return Vector2.zero;

        return dir.normalized;
    }

    public bool ReachedDestination()
    {
        if (!hasDestination)
            return true;

        return Vector2.Distance(self.position, currentDestination) <= reachedDestinationDistance;
    }

    private void OnPathComplete(Path path)
    {
        if (path == null)
            return;

        if (path.error)
        {
            Debug.LogWarning("Path error: " + path.errorLog, this);
            currentPath = null;
            currentWaypointIndex = 0;
            return;
        }

        currentPath = path;
        currentWaypointIndex = 0;

        Vector2 currentPos = self.position;

        while (currentWaypointIndex < currentPath.vectorPath.Count - 1 &&
               Vector2.Distance(currentPos, currentPath.vectorPath[currentWaypointIndex]) <= nextWaypointDistance)
        {
            currentWaypointIndex++;
        }
    }

    #region Basic API
    public void SetRepathRate(float newRate) => repathRate = newRate;
    public void SetNextWaypointDistance(float newDistance) => nextWaypointDistance = newDistance;
    public void SetReachedDestinationDistance(float newDistance) => reachedDestinationDistance = newDistance;

    public void ResetRepthRate() => repathRate = defaultRepathRate;
    public void ResetNextWaypointDistance() => nextWaypointDistance = defaultNextWaypointDistance;
    public void ResetReachedDestinationDistance() => reachedDestinationDistance = defaultReachedDestinationDistance;

    public float GetRepathRate() => repathRate;
    public float GetNextWaypointDistance() => nextWaypointDistance;
    public float GetReachedDestinationDistance() => reachedDestinationDistance;
    #endregion
}