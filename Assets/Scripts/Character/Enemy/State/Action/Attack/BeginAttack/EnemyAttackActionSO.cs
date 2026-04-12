using UnityEngine;
[CreateAssetMenu(menuName = "AI/UOP1 Style/Actions/Attack/EnemyBeginAttack")]
public class EnemyAttackActionSO : EnemyActionSO
{
    public override IAction<EnemyContext> CreateAction()
    {
        return new EnemyAttackAction();
    }
}
