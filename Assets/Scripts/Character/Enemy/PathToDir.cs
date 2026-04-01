using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class PathToDir : MonoBehaviour
{
    private Seeker seeker;
    private Transform self;

    [Header("Path Settings")]
    [SerializeField] private float repathRate = 0.35f;
    [SerializeField] private float nextWaypointDistance = 0.15f;
    [SerializeField] private float reachedDestinationDistance = 0.2f;
    
    private float defaultRepathRate;
    private float defaultNextWaypointDistance;
    private float defaultReachedDestinationDistance;

    private Path currentPath;
    private int currentWaypointIndex;
    private Vector2 currentDestination;
    private bool hasDestination;
    private float repathTimer;

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

        Vector2 dir = waypoint - currentPos;

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
    }

    #region ฺBasic API
    //Set
    public void SetRepathRate(float newRate) =>repathRate = newRate;
    public void SetNextWaypointDistance(float newDistance) => nextWaypointDistance = newDistance;
    public void SetReachedDestinationDistance(float newDistance) => reachedDestinationDistance = newDistance;

    //Reset
    public void ResetRepthRate() => repathRate = defaultRepathRate;
    public void ResetNextWaypointDistance() => nextWaypointDistance = defaultNextWaypointDistance;
    public void ResetReachedDestinationDistance() => reachedDestinationDistance = defaultReachedDestinationDistance;

    //Get
    public float GetRepathRate() => repathRate;
    public float GetNextWaypointDistance() => nextWaypointDistance;
    public float GetReachedDestinationDistance() => reachedDestinationDistance;

    #endregion
}