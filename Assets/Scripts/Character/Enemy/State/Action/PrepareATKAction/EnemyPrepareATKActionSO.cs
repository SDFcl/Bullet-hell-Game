using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Actions/Attack/EnemyPrepareATK")]public class EnemyPrepareATKActionSO : EnemyActionSO
{
    public override IAction<EnemyContext> CreateAction()
    {
        return new EnemyPrepareATKAction();
    }
}
