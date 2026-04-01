using System.Collections.Generic;

public class State<T> : IState<T>
{
    private readonly List<IAction<T>> _actions = new();
    private readonly List<Transition<T>> _transitions = new();

    public State(IEnumerable<IAction<T>> actions = null, IEnumerable<Transition<T>> transitions = null)
    {
        if (actions != null)
            _actions.AddRange(actions);

        if (transitions != null)
            _transitions.AddRange(transitions);
    }

    public void AddAction(IAction<T> action)
    {
        _actions.Add(action);
    }

    public void AddTransition(Transition<T> transition)
    {
        _transitions.Add(transition);
    }

    public IState<T> GetNextState(T ctx)
    {
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