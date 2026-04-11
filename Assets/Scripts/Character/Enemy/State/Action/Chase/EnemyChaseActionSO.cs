using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Actions/EnemyChase")]
public class EnemyChaseActionSO : EnemyActionSO
{
    public float facingThrshold;
    public override IAction<EnemyContext> CreateAction()
    {
        return new EnemyChaseAction(facingThrshold);
    }
}
