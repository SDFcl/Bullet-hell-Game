using UnityEngine;

public class GameStateChangedEvent : EventData
{
    public GameState PreviousState { get; }
    public GameState NewState { get; }

    public GameStateChangedEvent(GameState previous, GameState current)
    {
        PreviousState = previous;
        NewState = current;
    }
}
