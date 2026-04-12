using UnityEngine;

public class EnemyGateCooldownCondition : IResettableCondition<EnemyContext>
{
    private readonly float waitTime;
    private float startedAt;
    private bool counting;

    public EnemyGateCooldownCondition(float waitTime)
    {
        this.waitTime = waitTime;
    }

    public bool IsMet(EnemyContext ctx)
    {
        if (!counting)
        {
            startedAt = Time.time;
            counting = true;
        }

        return Time.time - startedAt >= waitTime;
    }

    public void Reset(EnemyContext ctx)
    {
        counting = false;
        startedAt = 0f;
    }
}
