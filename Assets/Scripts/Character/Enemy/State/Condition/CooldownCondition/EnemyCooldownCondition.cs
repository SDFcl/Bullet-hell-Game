using UnityEngine;

public class EnemyCooldownCondition : ICondition<EnemyContext>
{
    private readonly bool useRandomCooldown;
    private readonly float fixedWaitTime;
    private readonly float minWaitTime;
    private readonly float maxWaitTime;

    private float currentWaitTime;
    private float lastObservedTime = -1f;
    private bool hasInitializedCooldown;

    public EnemyCooldownCondition(float waitTime)
    {
        useRandomCooldown = false;
        fixedWaitTime = waitTime;
        currentWaitTime = waitTime;
    }

    public EnemyCooldownCondition(float minWaitTime, float maxWaitTime)
    {
        useRandomCooldown = true;
        this.minWaitTime = minWaitTime;
        this.maxWaitTime = maxWaitTime;
    }

    public bool IsMet(EnemyContext ctx)
    {
        float currentTime = ctx.Timer.TimeElapsed;

        // Timer ถูก reset ตอนเปลี่ยน state ใน EnemyController
        // ถ้าเวลาไหลย้อนกลับ แปลว่าเข้า state ใหม่แล้ว ให้สุ่ม cooldown ใหม่
        if (!hasInitializedCooldown || currentTime < lastObservedTime)
        {
            RollNextCooldown();
            hasInitializedCooldown = true;
        }

        lastObservedTime = currentTime;
        return currentTime >= currentWaitTime;
    }

    private void RollNextCooldown()
    {
        currentWaitTime = useRandomCooldown
            ? Random.Range(minWaitTime, maxWaitTime)
            : fixedWaitTime;
    }
}
