using System;
using System.Collections.Generic;

public static class EventBus
{
    private static readonly Dictionary<Type, Delegate> events = new();

    public static void Subscribe<T>(Action<T> listener) where T : EventData
    {
        Type eventType = typeof(T);
        if (!events.ContainsKey(eventType))
        {
            events[eventType] = null;
        }
        events[eventType] = Delegate.Combine(events[eventType], listener);
    }

    public static void Unsubscribe<T>(Action<T> listener) where T : EventData
    {
        Type eventType = typeof(T);
        if (events.ContainsKey(eventType))
        {
            events[eventType] = Delegate.Remove(events[eventType], listener);
        }
    }

    public static void Raise<T>(T eventData) where T : EventData
    {
        Type eventType = typeof(T);
        if (events.ContainsKey(eventType) && events[eventType] is Action<T> action)
        {
            action.Invoke(eventData);
        }
    }
}