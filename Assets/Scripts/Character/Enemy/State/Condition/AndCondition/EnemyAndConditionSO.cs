using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/And")]
public class EnemyAndConditionSO : EnemyConditionSO
{
    public EnemyConditionSO[] conditions;

    public override ICondition<EnemyContext> CreateCondition()
    {
        ICondition<EnemyContext>[] runtimeConditions = new ICondition<EnemyContext>[conditions.Length];

        for (int i = 0; i < conditions.Length; i++)
        {
            runtimeConditions[i] = conditions[i].CreateCondition();
        }

        return new EnemyAndCondition(runtimeConditions);
    }
}