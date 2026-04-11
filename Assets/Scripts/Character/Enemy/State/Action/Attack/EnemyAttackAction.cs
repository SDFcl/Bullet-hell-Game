using UnityEngine;

public class EnemyAttackAction : IAction<EnemyContext>
{
    public void OnEnter(EnemyContext ctx)
    {
        ctx.Movement.StopMovement();
        ctx.PathToDir.ClearDestination();

        ctx.Attack.TryAttack();
    }

    public void OnUpdate(EnemyContext ctx)
    {
        
    }

    public void OnExit(EnemyContext ctx)
    {
        
    }
}
