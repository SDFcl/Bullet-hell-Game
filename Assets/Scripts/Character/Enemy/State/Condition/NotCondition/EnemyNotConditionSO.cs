using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/Not")]
public class EnemyNotConditionSO : EnemyConditionSO
{
    public EnemyConditionSO condition;

    public override ICondition<EnemyContext> CreateCondition()
    {
        if (condition == null)
        {
            UnityEngine.Debug.LogError("NotConditionSO: condition is null");
            return null;
        }

        return new EnemyNotCondition(condition.CreateCondition());
    }
}