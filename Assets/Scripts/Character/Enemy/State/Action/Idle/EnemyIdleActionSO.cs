using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Actions/EnemyIdle")]
public class EnemyIdleActionSO : EnemyActionSO
{
    public override IAction<EnemyContext> CreateAction()
    {
        return new EnemyIdleAction();
    }
}
