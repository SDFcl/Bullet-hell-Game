using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/Distance")]
public class DistanceSO : EnemyConditionSO
{
    public float range;
    public ComparisonType comparison;

    public override ICondition<EnemyContext> CreateCondition()
    {
        return new Distance(range, comparison);
    }
}
