using UnityEngine;

[CreateAssetMenu(menuName = "AI/UOP1 Style/Actions/EnemyWander")]
public class EnemyWanderActionSO : EnemyActionSO
{
    public float wanderRadius = 4f;
    public float minWaitTime = 1f;
    public float maxWaitTime = 2.5f;
    public float wanderMoveSpeed = 3f;
    public LayerMask obstacleMask;

    public override IAction<EnemyContext> CreateAction()
    {
        return new EnemyWanderAction(wanderRadius, minWaitTime, maxWaitTime, wanderMoveSpeed, obstacleMask);
    }
}   
