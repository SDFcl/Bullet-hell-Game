using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D menuCursor;
    [SerializeField] private Texture2D gameplayCursor;
    [SerializeField] private Vector2 hotspot = Vector2.zero;

    void Start()
{
    UpdateCursor(GameStateManager.CurrentState);
}

private void OnEnable()
{
    EventBus.Subscribe<GameStateChangedEvent>(OnGameStateChanged);
}

private void OnDisable()
{
    EventBus.Unsubscribe<GameStateChangedEvent>(OnGameStateChanged);
}

private void OnGameStateChanged(GameStateChangedEvent e)
{
    UpdateCursor(e.NewState);
}

private void UpdateCursor(GameState state)
{
    switch (state)
    {
        case GameState.MainMenu:
            Cursor.SetCursor(menuCursor, hotspot, CursorMode.Auto);
            break;

        case GameState.GamePlay:
            Cursor.SetCursor(gameplayCursor, hotspot, CursorMode.Auto);
            break;

        default:
            Cursor.SetCursor(menuCursor, hotspot, CursorMode.Auto);
            break;
    }
}

}
