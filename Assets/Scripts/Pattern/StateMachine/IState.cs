using UnityEngine;

public interface IState<T>
{
    void OnEnter(T context);
    void OnUpdate(T context);
    void OnExit(T context);
}
public interface IAction<T>
{
    void OnEnter(T ctx);
    void OnUpdate(T ctx);
    void OnExit(T ctx);
}
public interface ICondition<T>
{
    bool IsMet(T ctx);
}
public class Transition<T>
{
    public ICondition<T> Condition { get; }
    public IState<T> TargetState { get; }

    public Transition(ICondition<T> condition, IState<T> targetState)
    {
        Condition = condition;
        TargetState = targetState;
    }
}
