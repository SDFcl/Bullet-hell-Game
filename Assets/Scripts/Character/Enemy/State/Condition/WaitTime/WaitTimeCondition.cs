using UnityEngine;

public class WaitTimeCondition : ICondition<EnemyContext>
{
    private readonly float _waitTime;
    public WaitTimeCondition(float waitTime)
    {
        _waitTime = waitTime;
    }

    public bool IsMet(EnemyContext ctx)
    {
        return ctx.Timer.TimeElapsed >= _waitTime;
    }
}
