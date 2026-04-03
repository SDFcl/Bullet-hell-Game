using UnityEngine;

public class EnemyNullCondition : ICondition<EnemyContext>
{
    public bool IsMet(EnemyContext ctx)
    {
        return true;
    }
}
