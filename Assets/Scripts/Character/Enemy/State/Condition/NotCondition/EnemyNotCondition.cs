using UnityEngine;

public class EnemyNotCondition : ICondition<EnemyContext>
{
    private readonly ICondition<EnemyContext> innerCondition;

    public EnemyNotCondition(ICondition<EnemyContext> innerCondition)
    {
        this.innerCondition = innerCondition;
    }

    public bool IsMet(EnemyContext ctx)
    {
        return !innerCondition.IsMet(ctx);
    }
}
