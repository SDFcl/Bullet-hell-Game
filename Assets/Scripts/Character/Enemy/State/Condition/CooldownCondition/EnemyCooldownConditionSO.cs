using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/State Cooldown")]
public class EnemyCooldownConditionSO : EnemyConditionSO
{
    [SerializeField] private bool useRandomCooldown = false;
    [SerializeField] private float cooldown = 0f;
    [SerializeField] private float minCooldown = 0f;
    [SerializeField] private float maxCooldown = 0f;

    public override ICondition<EnemyContext> CreateCondition()
    {
        if (useRandomCooldown)
        {
            return new EnemyCooldownCondition(minCooldown, maxCooldown);
        }

        return new EnemyCooldownCondition(cooldown);
    }
}
