using UnityEngine;
[CreateAssetMenu(menuName = "AI/UOP1 Style/Actions/EnemyPositioning")]
public class EnemyPositioningActionSO : EnemyActionSO
{
    public float preferredDistance = 2f;
    public float repathInterval = 0.5f;
    public override IAction<EnemyContext> CreateAction()
    {
        return new EnemyPositioningAction(preferredDistance, repathInterval);
    }
}
