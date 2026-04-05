using UnityEngine;

public class EnemyOnDeadCondition : ICondition<EnemyContext>
{
    public bool IsMet(EnemyContext ctx)
    {
        return ctx.Health.IsDead;
    }
}
