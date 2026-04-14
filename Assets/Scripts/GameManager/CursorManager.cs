using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D menuCursor;
    [SerializeField] private Texture2D gameplayCursor;
    [SerializeField] private Vector2 hotspot = Vector2.zero;

    public void UpdateCursor()
    {
        switch (GameStateManager.CurrentState)
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
