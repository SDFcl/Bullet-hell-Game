using UnityEngine;
[CreateAssetMenu(menuName = "AI/UOP1 Style/Actions/EnemyDead")]
public class EnemyDeadActionSO : EnemyActionSO
{
    public override IAction<EnemyContext> CreateAction()
    {
        return new EnemyDeadAction();
    }
}
