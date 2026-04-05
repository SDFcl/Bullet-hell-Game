using UnityEngine;

public class EnemyPositioningAction : IAction<EnemyContext>
{
    private readonly float preferredDistance;
    private readonly float repathInterval;

    private float repathTimer;
    private Vector2 currentPositionTarget;

    public EnemyPositioningAction(
        float preferredDistance = 2f,
        float repathInterval = 0.5f)
    {
        this.preferredDistance = preferredDistance;
        this.repathInterval = repathInterval;
    }

    public void OnEnter(EnemyContext ctx)
    {
        repathTimer = 0f;
        currentPositionTarget = CalculatePositionTarget(ctx);
    }

    public void OnUpdate(EnemyContext ctx)
    {
        if (ctx.Target == null || ctx.Movement == null) return;

        // รีคำนวณตำแหน่งเป็นช่วง ๆ
        repathTimer -= Time.deltaTime;
        if (repathTimer <= 0f)
        {
            repathTimer = repathInterval;
            currentPositionTarget = CalculatePositionTarget(ctx);
        }

        MoveToPosition(ctx, currentPositionTarget);
    }

    public void OnExit(EnemyContext ctx)
    {
        if (ctx.Movement != null)
            ctx.Movement.StopMovement();
    }

    // 🔥 หาตำแหน่งด้านข้าง player
    private Vector2 CalculatePositionTarget(EnemyContext ctx)
    {
        Vector2 enemyPos = ctx.Self.position;
        Vector2 targetPos = ctx.Target.position;

        Vector2 toEnemy = (enemyPos - targetPos).normalized;

        if (toEnemy == Vector2.zero)
            toEnemy = Vector2.right;

        Vector2 sideDir = Vector2.Perpendicular(toEnemy).normalized;

        // กัน flip ซ้ายขวาแปลก ๆ
        float sideSign = Vector2.Dot(sideDir, enemyPos - targetPos) >= 0f ? 1f : -1f;
        sideDir *= sideSign;

        return targetPos + sideDir * preferredDistance;
    }

    private void MoveToPosition(EnemyContext ctx, Vector2 targetPos)
    {
        if (ctx.PathToDir == null) return;

        ctx.PathToDir.SetDestination(targetPos);
        if (ctx.PathToDir.ReachedDestination())
        {
            ctx.Movement.StopMovement();
            return;
        }
        ctx.Movement.SetMoveInput(ctx.PathToDir.GetDirection());

        // optional: facing ใช้ direction จาก path
        Vector2 dir = (ctx.Target.position - ctx.Self.position).normalized;

        ctx.Facing.SetDirection(dir.x);
        ctx.AimPivot.SetDirection(dir);
    }
}