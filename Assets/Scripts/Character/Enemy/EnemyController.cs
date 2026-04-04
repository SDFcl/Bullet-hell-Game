using UnityEngine;
using Pathfinding;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private EnemyStateGraphSO stateGraph;

    private EnemyContext ctx;
    private StateMachine<EnemyContext> stateMachine;

    private void Awake()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;
        ctx = new EnemyContext(
            self: transform,
            target: target,
            health: GetComponent<Health>(),
            mana: GetComponent<Mana>(),
            movement: GetComponent<Movement>(),
            aimPivot: GetComponentInChildren<AimPivot2D>(),
            facing: GetComponent<Facing2D>(),
            pathToDir: GetComponent<PathToDir>(),
            lineOfSight: GetComponent<LineOfSight2D>(),
            attack: GetComponent<Attack>()
        );

        stateMachine = new StateMachine<EnemyContext>(ctx);

        IState<EnemyContext> startState = EnemyStateGraphBuilder.Build(stateGraph);
        stateMachine.Initialize(startState);

        stateMachine.OnStateChanged += HandleStateChanged;
    }

    void OnDestroy()
    {
        stateMachine.OnStateChanged -= HandleStateChanged;
    }

    private void Update()
    {
        ctx.Timer.Tick(Time.deltaTime);
        stateMachine.Tick();
    }
    private void HandleStateChanged(IState<EnemyContext> prev, IState<EnemyContext> next)
    {
        ctx.Timer.ResetTimer();
    }
}