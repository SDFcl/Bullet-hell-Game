using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/Wait Time")]
public class EnemyCooldownConditionSO : EnemyConditionSO
{
    public float cooldown;

    public override ICondition<EnemyContext> CreateCondition()
    {
        return new EnemyCooldownCondition(cooldown);
    }
}
