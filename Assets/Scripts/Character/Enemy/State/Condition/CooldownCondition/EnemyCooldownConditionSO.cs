using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/State Cooldown")]
public class EnemyCooldownConditionSO : EnemyConditionSO
{
    public float cooldown;

    public override ICondition<EnemyContext> CreateCondition()
    {
        return new EnemyCooldownCondition(cooldown);
    }
}
