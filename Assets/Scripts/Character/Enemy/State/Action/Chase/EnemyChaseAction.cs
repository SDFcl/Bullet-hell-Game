using UnityEngine;
public class EnemyChaseAction : IAction<EnemyContext>
{
    private readonly float _facingThreshold = 0.5f;
    public EnemyChaseAction(float facingThreshold)
    {
        _facingThreshold = facingThreshold;
    }
    public void OnEnter(EnemyContext ctx)
    {
        ctx.Facing.SetThreshold(_facingThreshold);
    }

    public void OnUpdate(EnemyContext ctx)
    {
        if (ctx.Target == null)
            return;

        ctx.PathToDir.SetDestination(ctx.Target.position);

        Vector2 dir = ctx.PathToDir.GetDirection();
        Vector2 dirToTarget = (ctx.Target.position - ctx.Self.position).normalized;

        ctx.Facing?.SetDirection(dirToTarget.x);
        ctx.AimPivot?.SetDirection(dirToTarget);

        // เปลี่ยนบรรทัดนี้ตาม API ของ Movement จริง
        ctx.Movement.SetMoveInput(dir.normalized);
    }

    public void OnExit(EnemyContext ctx)
    {
        //Debug.Log("Exit Chase");
        ctx.Facing.ResetThers();
        ctx.PathToDir.ClearDestination();
        ctx.Movement.StopMovement();
    }
}

