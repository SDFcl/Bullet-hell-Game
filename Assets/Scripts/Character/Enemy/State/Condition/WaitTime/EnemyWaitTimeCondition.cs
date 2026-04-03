using UnityEngine;

public class EnemyWaitTimeCondition : ICondition<EnemyContext>
{
    private readonly float _waitTime;
    public EnemyWaitTimeCondition(float waitTime)
    {
        _waitTime = waitTime;
    }

    public bool IsMet(EnemyContext ctx)
    {
        return ctx.Timer.TimeElapsed >= _waitTime;
    }
}
