using UnityEngine;

public class EnemyAttackAction : IAction<EnemyContext>
{
    public void OnEnter(EnemyContext ctx)
    {
        bool selected = true;

        if (ctx.Self.TryGetComponent<EnemyAttackSelector>(out var selector))
        {
            selected = selector.SelectRandomAttack();
            Debug.Log($"[EnemyAttackAction] selected = {selected}, currentIndex = {selector.Currentindex}");
        }

        if (!selected) return;

        ctx.Attack.RefreshWeapon();
        ctx.Attack.TryAttack();
    }

    public void OnUpdate(EnemyContext ctx)
    {
        
    }

    public void OnExit(EnemyContext ctx)
    {
        
    }
}
