using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/Distance")]
public class EnemyDistanceConditionSO : EnemyConditionSO
{
    public float range;
    public ComparisonType comparison;

    public override ICondition<EnemyContext> CreateCondition()
    {
        return new EnemyDistanceCondition(range, comparison);
    }
}
