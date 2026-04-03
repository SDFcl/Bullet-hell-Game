using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/Can See Target")]
public class EnemySeeTargetConditionSO : EnemyConditionSO
{
    public override ICondition<EnemyContext> CreateCondition()
    {
        return new EnemySeeTargetCondition();
    }
}