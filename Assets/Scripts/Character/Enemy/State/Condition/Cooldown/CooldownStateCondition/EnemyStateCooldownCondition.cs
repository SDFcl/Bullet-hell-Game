using UnityEngine;

public class EnemyStateCooldownCondition : ICondition<EnemyContext>
{
    private readonly float _waitTime;

    public EnemyStateCooldownCondition(float waitTime)
    {
        _waitTime = waitTime;
    }

    public bool IsMet(EnemyContext ctx)
    {
        return ctx.Timer.TimeElapsed >= _waitTime;
    }
}
