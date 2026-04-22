using UnityEngine;
[CreateAssetMenu(menuName = "AI/UOP1 Style/Actions/EnemyEntry")]
public class EnemyEntryActionSO : EnemyActionSO
{
    public override IAction<EnemyContext> CreateAction()
    {
        return new EnemyEntryAction();
    }
}
