using System.Collections.Generic;

public class State<T> : IState<T>
{
    #region Fields

    private readonly List<IAction<T>> _actions = new();
    private readonly List<Transition<T>> _transitions = new();
    private readonly List<Transition<T>> _globalTransitions = new();

    #endregion

    public State(IEnumerable<IAction<T>> actions = null, IEnumerable<Transition<T>> transitions = null)
    {
        if (actions != null)
            _actions.AddRange(actions);

        if (transitions != null)
            _transitions.AddRange(transitions);
    }

    public void AddAction(IAction<T> action)
    {
        if (action == null)
            return;

        _actions.Add(action);
    }

    public void AddTransition(Transition<T> transition)
    {
        if (transition == null)
            return;

        _transitions.Add(transition);
    }

    public void AddGlobalTransition(Transition<T> transition)
    {
        if (transition == null)
            return;

        _globalTransitions.Add(transition);
    }

    public IState<T> GetNextState(T ctx)
    {
        // เช็ก Any / Global ก่อน
        foreach (var transition in _globalTransitions)
        {
            if (transition.Condition.IsMet(ctx))
                return transition.TargetState;
        }

        // แล้วค่อยเช็ก transition ปกติ
        foreach (var transition in _transitions)
        {
            if (transition.Condition.IsMet(ctx))
                return transition.TargetState;
        }

        return null;
    }

    public void OnEnter(T ctx)
    {
        foreach (var action in _actions)
            action.OnEnter(ctx);
    }

    public void OnUpdate(T ctx)
    {
        foreach (var action in _actions)
            action.OnUpdate(ctx);
    }

    public void OnExit(T ctx)
    {
        foreach (var action in _actions)
            action.OnExit(ctx);
    }
}