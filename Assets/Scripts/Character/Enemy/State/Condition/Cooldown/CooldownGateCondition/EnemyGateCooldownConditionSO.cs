using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/Cooldown/Gate Cooldown")]
public class EnemyGateCooldownConditionSO : EnemyConditionSO
{
    public float cooldown;

    public override ICondition<EnemyContext> CreateCondition()
    {
        return new EnemyGateCooldownCondition(cooldown);
    }
}
