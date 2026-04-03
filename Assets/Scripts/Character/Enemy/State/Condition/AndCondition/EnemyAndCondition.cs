public class EnemyAndCondition : ICondition<EnemyContext>
{
    private readonly ICondition<EnemyContext>[] conditions;

    public EnemyAndCondition(params ICondition<EnemyContext>[] conditions)
    {
        this.conditions = conditions;
    }

    public bool IsMet(EnemyContext ctx)
    {
        foreach (var condition in conditions)
        {
            if (!condition.IsMet(ctx))
                return false;
        }

        return true;
    }
}