using UnityEngine;
[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/On Dead")]
public class EnemyOnDeadConditionSO : EnemyConditionSO
{
    public override ICondition<EnemyContext> CreateCondition()
    {
        return new EnemyOnDeadCondition();
    }
}
