using UnityEngine;
public class ChaseAction : IAction<EnemyContext>
{
    public void OnEnter(EnemyContext ctx)
    {
        Debug.Log("Enter Chase");
    }

    public void OnUpdate(EnemyContext ctx)
    {
        if (ctx.Target == null)
            return;

        ctx.PathToDir.SetDestination(ctx.Target.position);

        Vector2 dir = ctx.PathToDir.GetDirection();

        ctx.Facing?.SetDirection(dir.x);

        // เปลี่ยนบรรทัดนี้ตาม API ของ Movement จริง
        ctx.Movement.SetmoveInput(dir.normalized);
    }

    public void OnExit(EnemyContext ctx)
    {
        Debug.Log("Exit Chase");
        ctx.PathToDir.ClearDestination();
        ctx.Movement.StopMovement();
    }
}

