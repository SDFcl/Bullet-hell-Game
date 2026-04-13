using UnityEngine;

public class EnemyPositioningAction : IAction<EnemyContext>
{
    private readonly float preferredDistance;
    private readonly float repathInterval;

    private float repathTimer;
    private Vector2 currentPositionTarget;
    private float sideSign = 1f;

    public EnemyPositioningAction(
        float preferredDistance = 2f,
        float repathInterval = 0.5f)
    {
        this.preferredDistance = preferredDistance;
        this.repathInterval = repathInterval;
    }

    public void OnEnter(EnemyContext ctx)
    {
        if (ctx.Target == null) return;

        repathTimer = 0f;

        // เลือกว่าจะยืนฝั่งซ้ายหรือขวาของเป้าหมายครั้งเดียวต่อการเข้า state
        sideSign = Random.value < 0.5f ? -1f : 1f;
        currentPositionTarget = CalculatePositionTarget(ctx);
    }

    public void OnUpdate(EnemyContext ctx)
    {
        if (ctx.Target == null || ctx.Movement == null || ctx.PathToDir == null)
            return;

        repathTimer -= Time.deltaTime;
        if (repathTimer <= 0f)
        {
            repathTimer = repathInterval;
            currentPositionTarget = CalculatePositionTarget(ctx);
            ctx.PathToDir.SetDestination(currentPositionTarget);
        }

        MoveToPosition(ctx);
        UpdateFacing(ctx);
    }

    public void OnExit(EnemyContext ctx)
    {
        ctx.Movement?.StopMovement();
        ctx.PathToDir?.ClearDestination();
    }

    private Vector2 CalculatePositionTarget(EnemyContext ctx)
    {
        Vector2 enemyPos = ctx.Self.position;
        Vector2 targetPos = ctx.Target.position;

        Vector2 toEnemy = enemyPos - targetPos;
        if (toEnemy.sqrMagnitude <= 0.0001f)
        {
            toEnemy = Vector2.right;
        }

        Vector2 sideDir = Vector2.Perpendicular(toEnemy.normalized) * sideSign;
        return targetPos + sideDir * preferredDistance;
    }

    private void MoveToPosition(EnemyContext ctx)
    {
        if (ctx.PathToDir.ReachedDestination())
        {
            ctx.Movement.StopMovement();
            return;
        }

        Vector2 moveDirection = ctx.PathToDir.GetDirection();
        ctx.Movement.SetMoveInput(moveDirection);
    }

    private void UpdateFacing(EnemyContext ctx)
    {
        Vector2 lookDir = (ctx.Target.position - ctx.Self.position).normalized;

        if (ctx.Facing != null)
        {
            ctx.Facing.SetDirection(lookDir.x);
        }

        if (ctx.AimPivot != null)
        {
            ctx.AimPivot.SetDirection(lookDir);
        }
    }
}
