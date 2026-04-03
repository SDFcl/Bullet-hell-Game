using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/Wait Time")]
public class EnemyWaitTimeConditionSO : EnemyConditionSO
{
    public float waitTime;

    public override ICondition<EnemyContext> CreateCondition()
    {
        return new EnemyWaitTimeCondition(waitTime);
    }
}
