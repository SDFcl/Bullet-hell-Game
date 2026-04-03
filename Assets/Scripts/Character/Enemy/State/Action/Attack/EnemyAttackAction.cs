using UnityEngine;

public class EnemyAttackAction : IAction<EnemyContext>
{
    public void OnEnter(EnemyContext ctx)
    {
        Debug.Log("Enter Attack");
        ctx.Movement.StopMovement();
        ctx.Attack.TryAttack();
    }

    public void OnUpdate(EnemyContext ctx)
    {
        
    }

    public void OnExit(EnemyContext ctx)
    {
        
    }
}
