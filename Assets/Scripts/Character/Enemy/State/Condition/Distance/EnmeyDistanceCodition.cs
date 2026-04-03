using UnityEngine;
public enum ComparisonType
{
    Less,
    Greater,
    LessOrEqual,
    GreaterOrEqual
}
public class EnemyDistanceCondition : ICondition<EnemyContext>
{
    private readonly float range;
    private readonly ComparisonType comparison;

    public EnemyDistanceCondition(float range, ComparisonType comparison)
    {
        this.range = range;
        this.comparison = comparison;
    }

    public bool IsMet(EnemyContext ctx)
    {
        if (ctx.Target == null)
            return false;

        float distance = Vector2.Distance(ctx.Self.position, ctx.Target.position);

        return Compare(distance);
    }

    private bool Compare(float distance)
    {
        switch (comparison)
        {
            case ComparisonType.Less:
                return distance < range;

            case ComparisonType.Greater:
                return distance > range;

            case ComparisonType.LessOrEqual:
                return distance <= range;

            case ComparisonType.GreaterOrEqual:
                return distance >= range;

            default:
                return false;
        }
    }
}