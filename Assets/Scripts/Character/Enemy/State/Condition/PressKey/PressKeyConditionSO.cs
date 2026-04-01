using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Conditions/Press Key")]
public class PressKeyConditionSO : EnemyConditionSO
{
    public KeyCode keyCode;

    public override ICondition<EnemyContext> CreateCondition()
    {
        return new PressKeyCondition(keyCode);
    }
}
