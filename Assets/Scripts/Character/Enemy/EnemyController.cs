using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private EnemyStateGraphSO stateGraph;

    private EnemyContext ctx;
    private StateMachine<EnemyContext> stateMachine;

    private void Awake()
    {
        ctx = new EnemyContext(
            self: transform,
            target: target,
            health: GetComponent<Health>(),
            mana: GetComponent<Mana>(),
            movement: GetComponent<Movement>(),
            aimPivot: GetComponentInChildren<AimPivot2D>(),
            facing: GetComponent<Facing2D>()
        );

        stateMachine = new StateMachine<EnemyContext>(ctx);

        IState<EnemyContext> startState = EnemyStateGraphBuilder.Build(stateGraph);
        stateMachine.Initialize(startState);
    }

    private void Update()
    {
        stateMachine.Tick();
    }
}