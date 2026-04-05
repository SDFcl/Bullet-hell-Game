using UnityEngine;

public class EnemyDeadAction : IAction<EnemyContext>
{
    public void OnEnter(EnemyContext ctx)
    {
        Debug.Log("Enemy Dead");
        ctx.Movement.StopMovement();
        ctx.Get<Drop>()?.DropItem();
    }
    public void OnUpdate(EnemyContext ctx)
    {
        
    }

    public void OnExit(EnemyContext ctx)
    {
        
    }
}
