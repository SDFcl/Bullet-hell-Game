using UnityEngine;

public class EnemyCooldownCondition : ICondition<EnemyContext>
{
    private readonly float _waitTime;
    public EnemyCooldownCondition(float waitTime)
    {
        _waitTime = waitTime;
    }

    public bool IsMet(EnemyContext ctx)
    {
        return ctx.Timer.TimeElapsed >= _waitTime;
    }
}
