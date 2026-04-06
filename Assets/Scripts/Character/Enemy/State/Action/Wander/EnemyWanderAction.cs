using UnityEngine;

public class EnemyWanderAction : IAction<EnemyContext>
{
    private readonly float wanderRadius;
    private readonly float minWaitTime;
    private readonly float maxWaitTime;
    private readonly float wanderMoveSpeed;
    private readonly LayerMask obstacleMask;

    private const float ClearanceRadius = 0.4f;
    private const int MaxPickAttempts = 12;

    private readonly float stuckCheckThreshold = 0.05f;
    private readonly float stuckTimeToReroll = 0.8f;
    private readonly float stuckCheckInterval = 0.2f;

    private float waitTimer;
    private bool isWaiting;

    private Vector2 lastPosition;
    private float stuckTimer;
    private float stuckCheckTimer;

    public EnemyWanderAction(
        float wanderRadius,
        float minWaitTime,
        float maxWaitTime,
        float wanderMoveSpeed,
        LayerMask obstacleMask = default)
    {
        this.wanderRadius = wanderRadius;
        this.minWaitTime = minWaitTime;
        this.maxWaitTime = maxWaitTime;
        this.wanderMoveSpeed = wanderMoveSpeed;
        this.obstacleMask = obstacleMask;
    }

    public void OnEnter(EnemyContext ctx)
    {
        Debug.Log("Enter Wander");

        ResetRuntimeState(ctx);

        ctx.Movement.SetMoveSpeed(wanderMoveSpeed);

        PickNewDestination(ctx);
    }

    public void OnUpdate(EnemyContext ctx)
    {
        if (HandleWaiting(ctx))
            return;

        MoveAlongPath(ctx);

        if (ctx.PathToDir.ReachedDestination())
        {
            StartWaiting(ctx);
            return;
        }

        CheckStuck(ctx);
    }

    public void OnExit(EnemyContext ctx)
    {
        Debug.Log("Exit Wander");

        ctx.Movement.ResetMoveSpeed();
        ctx.PathToDir.ClearDestination();
        ctx.Movement.StopMovement();
    }

    private bool HandleWaiting(EnemyContext ctx)
    {
        if (!isWaiting)
            return false;

        waitTimer -= Time.deltaTime;
        ctx.Movement.StopMovement();

        if (waitTimer <= 0f)
        {
            isWaiting = false;
            PickNewDestination(ctx);
        }

        return true;
    }

    private void MoveAlongPath(EnemyContext ctx)
    {
        Vector2 direction = ctx.PathToDir.GetDirection();

        ctx.MoveTo(direction);
    }

    private void PickNewDestination(EnemyContext ctx)
    {
        Vector2 origin = ctx.Self.position;

        for (int i = 0; i < MaxPickAttempts; i++)
        {
            Vector2 candidate = origin + Random.insideUnitCircle * wanderRadius;

            if (IsBlocked(candidate))
                continue;

            ctx.PathToDir.SetDestination(candidate);
            ResetStuckTracking(ctx);
            return;
        }

        // fallback
        ctx.PathToDir.ClearDestination();
        ctx.Movement.StopMovement();
    }

    private bool IsBlocked(Vector2 position)
    {
        return Physics2D.OverlapCircle(position, ClearanceRadius, obstacleMask);
    }

    private void StartWaiting(EnemyContext ctx)
    {
        isWaiting = true;
        waitTimer = Random.Range(minWaitTime, maxWaitTime);

        ResetStuckTracking(ctx);
        ctx.Movement.StopMovement();
    }

    private void CheckStuck(EnemyContext ctx)
    {
        stuckCheckTimer += Time.deltaTime;

        if (stuckCheckTimer < stuckCheckInterval)
            return;

        Vector2 currentPosition = ctx.Self.position;
        float movedDistance = Vector2.Distance(currentPosition, lastPosition);

        if (movedDistance < stuckCheckThreshold)
        {
            stuckTimer += stuckCheckInterval;

            if (stuckTimer >= stuckTimeToReroll)
            {
                PickNewDestination(ctx);
                return;
            }
        }
        else
        {
            stuckTimer = 0f;
        }

        lastPosition = currentPosition;
        stuckCheckTimer = 0f;
    }

    private void ResetRuntimeState(EnemyContext ctx)
    {
        isWaiting = false;
        waitTimer = 0f;
        ResetStuckTracking(ctx);
    }

    private void ResetStuckTracking(EnemyContext ctx)
    {
        stuckTimer = 0f;
        stuckCheckTimer = 0f;
        lastPosition = ctx.Self.position;
    }
}