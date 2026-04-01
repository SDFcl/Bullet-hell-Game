using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Actions/Chase")]
public class ChaseActionSO : EnemyActionSO
{
    public override IAction<EnemyContext> CreateAction()
    {
        return new ChaseAction();
    }
}
