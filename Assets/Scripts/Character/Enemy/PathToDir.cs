using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class PathToDir : MonoBehaviour
{
    [SerializeField] private float repathRate = 0.5f;
    [SerializeField] private float nextWaypointDistance = 0.6f;
    [SerializeField] private float reachedDistance = 0.4f;

    private Seeker seeker;
    private Transform self;

    private Path currentPath;
    private int currentWaypointIndex;

    private Vector2 destination;
    private bool hasDestination;
    private float repathTimer;

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
        destination = newDestination;
        hasDestination = true;
    }

    public void ClearDestination()
    {
        hasDestination = false;
        currentPath = null;
        currentWaypointIndex = 0;
    }

    public Vector2 GetDirection()
    {
        return CalculateDirection();
    }

    public bool ReachedDestination()
    {
        if (!hasDestination) return true;
        if (currentPath == null || currentPath.vectorPath == null || currentPath.vectorPath.Count == 0) return true;

        Vector2 endPoint = currentPath.vectorPath[currentPath.vectorPath.Count - 1];
        return Vector2.Distance(self.position, endPoint) <= reachedDistance;
    }

    private void UpdatePath()
    {
        if (!hasDestination || !seeker.IsDone()) return;

        repathTimer -= Time.deltaTime;
        if (repathTimer > 0f) return;

        repathTimer = repathRate;
        seeker.StartPath(self.position, destination, OnPathComplete);
    }

    private void OnPathComplete(Path path)
    {
        if (path == null || path.error)
        {
            currentPath = null;
            currentWaypointIndex = 0;
            return;
        }

        currentPath = path;
        currentWaypointIndex = 0;
    }

    private Vector2 CalculateDirection()
    {
        if (currentPath == null || currentPath.vectorPath == null || currentPath.vectorPath.Count <= 1)
            return Vector2.zero;

        Vector2 currentPos = self.position;
        Vector2 endPoint = currentPath.vectorPath[currentPath.vectorPath.Count - 1];

        if (Vector2.Distance(currentPos, endPoint) <= reachedDistance)
            return Vector2.zero;

        while (currentWaypointIndex < currentPath.vectorPath.Count - 1)
        {
            Vector2 waypoint = currentPath.vectorPath[currentWaypointIndex];
            if (Vector2.Distance(currentPos, waypoint) > nextWaypointDistance) break;
            currentWaypointIndex++;
        }

        if (currentWaypointIndex >= currentPath.vectorPath.Count)
            return Vector2.zero;

        Vector2 dir = (Vector2)currentPath.vectorPath[currentWaypointIndex] - currentPos;
        return dir.sqrMagnitude <= 0.0001f ? Vector2.zero : dir.normalized;
    }
}
