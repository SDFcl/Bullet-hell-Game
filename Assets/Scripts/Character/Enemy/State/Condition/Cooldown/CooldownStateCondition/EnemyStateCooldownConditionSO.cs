using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/Cooldown/State Cooldown")]
public class EnemyStateCooldownConditionSO : EnemyConditionSO
{
    public float cooldown;

    public override ICondition<EnemyContext> CreateCondition()
    {
        return new EnemyStateCooldownCondition(cooldown);
    }
}
