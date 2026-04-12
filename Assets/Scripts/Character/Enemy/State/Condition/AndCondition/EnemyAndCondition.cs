public enum EnemyAndMode
    {
        All,SequentialGate
    }

public class EnemyAndCondition : ICondition<EnemyContext>
{
    private readonly ICondition<EnemyContext>[] conditions;
    private readonly EnemyAndMode mode;

    public EnemyAndCondition(EnemyAndMode mode, params ICondition<EnemyContext>[] conditions)
    {
        this.mode = mode;
        this.conditions = conditions;
    }

    public bool IsMet(EnemyContext ctx)
    {
        switch (mode)
        {
            case EnemyAndMode.SequentialGate:
                return IsSequentialGateMet(ctx);

            case EnemyAndMode.All:
            default:
                return IsAllMet(ctx);
        }
    }

    private bool IsAllMet(EnemyContext ctx)
    {
        foreach (var condition in conditions)
        {
            if (!condition.IsMet(ctx))
                return false;
        }

        return true;
    }

    private bool IsSequentialGateMet(EnemyContext ctx)
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            if (!conditions[i].IsMet(ctx))
            {
                ResetRemainingConditions(ctx, i + 1);
                return false;
            }
        }

        return true;
    }

    private void ResetRemainingConditions(EnemyContext ctx, int startIndex)
    {
        for (int i = startIndex; i < conditions.Length; i++)
        {
            if (conditions[i] is IResettableCondition<EnemyContext> resettable)
                resettable.Reset(ctx);
        }
    }
}