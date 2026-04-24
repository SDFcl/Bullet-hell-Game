using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D menuCursor;
    [SerializeField] private Vector2 hotspot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(menuCursor, hotspot, CursorMode.Auto);
    }
}
