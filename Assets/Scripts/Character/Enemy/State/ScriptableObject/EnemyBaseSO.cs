using UnityEngine;

public abstract class EnemyActionSO : ScriptableObject
{
    public abstract IAction<EnemyContext> CreateAction();
}
public abstract class EnemyConditionSO : ScriptableObject
{
    public abstract ICondition<EnemyContext> CreateCondition();
}
