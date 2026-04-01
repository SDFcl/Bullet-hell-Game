using UnityEngine;

public class WanderAction : IAction<EnemyContext>
{
    private readonly float wanderRadius;
    private readonly float minWaitTime;
    private readonly float maxWaitTime;
    private readonly float wanderMoveSpeed;
    private readonly LayerMask obstacleMask;

    private float waitTimer;
    private bool isWaiting;

    private Vector2 lastPosition;
    private float stuckTimer;
    private float stuckCheckTimer;

    private readonly float stuckCheckThreshold = 0.05f;
    private readonly float stuckTimeToReroll = 0.8f;
    private readonly float stuckCheckInterval = 0.2f;
    private readonly float reachedDestinationDistance = 1f;

    public WanderAction(float wanderRadius, float minWaitTime, float maxWaitTime,float wanderMoveSpeed, LayerMask obstacleMask = default)
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

        // DefaultSettings
        isWaiting = false;
        waitTimer = 0f;
        stuckTimer = 0f;
        stuckCheckTimer = 0f;
        lastPosition = ctx.Self.position;

        ctx.PathToDir.SetReachedDestinationDistance(reachedDestinationDistance);
        ctx.Movement.SetMoveSpeed(wanderMoveSpeed);

        PickNewDestination(ctx);
    }

    public void OnUpdate(EnemyContext ctx)
    {
        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            ctx.Movement.StopMovement();

            if (waitTimer <= 0f)
            {
                isWaiting = false;
                PickNewDestination(ctx);
            }

            return;
        }

        Vector2 dir = ctx.PathToDir.GetDirection();

        ctx.Facing?.SetDirection(dir.x);
        ctx.Movement.SetmoveInput(dir);

        if (ctx.PathToDir.ReachedDestination())
        {
            StartWaiting(ctx);
        }
        else
        {
            CheckStuck(ctx);
        }
    }

    public void OnExit(EnemyContext ctx)
    {
        Debug.Log("Exit Wander");

        ctx.PathToDir.ResetReachedDestinationDistance();
        ctx.Movement.ResetMoveSpeed();

        ctx.PathToDir.ClearDestination();
        ctx.Movement.StopMovement();
    }

    private void PickNewDestination(EnemyContext ctx)
    {
        Vector2 center = ctx.Self.position;
        float clearanceRadius = 0.4f;

        for (int i = 0; i < 12; i++)
        {
            Vector2 candidate = center + Random.insideUnitCircle * wanderRadius;

            bool blocked = Physics2D.OverlapCircle(candidate, clearanceRadius, obstacleMask);

            if (!blocked)
            {
                ctx.PathToDir.SetDestination(candidate, true);

                stuckTimer = 0f;
                stuckCheckTimer = 0f;
                lastPosition = ctx.Self.position;
                return;
            }
        }

        // fallback
        ctx.PathToDir.ClearDestination();
        ctx.Movement.StopMovement();
    }

    private void StartWaiting(EnemyContext ctx)
    {
        isWaiting = true;
        waitTimer = Random.Range(minWaitTime, maxWaitTime);

        stuckTimer = 0f;
        stuckCheckTimer = 0f;
        lastPosition = ctx.Self.position;

        ctx.Movement.StopMovement();
    }

    private void CheckStuck(EnemyContext ctx)
    {
        stuckCheckTimer += Time.deltaTime;

        if (stuckCheckTimer < stuckCheckInterval)
            return;

        Vector2 currentPos = ctx.Self.position;
        float movedDistance = Vector2.Distance(currentPos, lastPosition);

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

        lastPosition = currentPos;
        stuckCheckTimer = 0f;
    }
}