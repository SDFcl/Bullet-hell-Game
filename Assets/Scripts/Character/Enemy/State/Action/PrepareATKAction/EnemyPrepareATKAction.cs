using UnityEngine;

public class EnemyPrepareATKAction : IAction<EnemyContext>
{
    public void OnEnter(EnemyContext ctx)
    {
        //Debug.Log("EnterExit");
        ctx.PathToDir.ClearDestination();
        ctx.Movement.StopMovement();
    }

    public void OnUpdate(EnemyContext ctx)
    {
        Vector2 dirToTarget = (ctx.Target.position - ctx.Self.position).normalized;
        ctx.AimPivot?.SetDirection(dirToTarget);
        ctx.Facing?.SetDirection(dirToTarget.x);
    }

    public void OnExit(EnemyContext ctx)
    {
        //Debug.Log("PrepareExit");
    }
}
