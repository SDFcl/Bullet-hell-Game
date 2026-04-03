using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/Null")]
public class EnemyNulllConditionSO : EnemyConditionSO
{
    public override ICondition<EnemyContext> CreateCondition()
    {
        return new EnemyNullCondition();
    }
}
