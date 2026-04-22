using UnityEngine;

public class EnemyEntryAction : IAction<EnemyContext>
{
    public void OnEnter(EnemyContext ctx)
    {
        ctx.Movement.StopMovement();
        ctx.Health.EnableIgnoreDamage(true);
    }
    public void OnUpdate(EnemyContext ctx)
    {
        
    }

    public void OnExit(EnemyContext ctx)
    {
        ctx.Health.EnableIgnoreDamage(false);
    }
}
