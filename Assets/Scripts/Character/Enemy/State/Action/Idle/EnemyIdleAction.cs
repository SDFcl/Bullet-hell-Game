using UnityEngine;

public class EnemyIdleAction : IAction<EnemyContext>
{
    public void OnEnter(EnemyContext ctx)
    {
        ctx.PathToDir.ClearDestination();
        ctx.Movement.StopMovement();
    }

    public void OnUpdate(EnemyContext ctx)
    {
        // idle logic
    }

    public void OnExit(EnemyContext ctx)
    {
        Debug.Log("Exit Idle");
    }
}