using UnityEngine;

public class GameFacade : MonoBehaviour
{
    private AstarPath pathfinder;
    private LevelManager levelManager;
    private CursorManager cursor;
    private void Awake()
    {
        levelManager = GetComponentInChildren<LevelManager>();
        pathfinder = GetComponentInChildren<AstarPath>();
        cursor = GetComponentInChildren<CursorManager>();
    }
    private void Start()
    {
        GameLoad();
        GameStart();
    }

    public void GameLoad()
    {
        cursor?.UpdateCursor();
        levelManager?.LoadLevel();
    }
    public void GameStart()
    {
        levelManager?.SpawnLevel();
        pathfinder?.Scan();

        // Load player inventory after spawning level
        Inventory playerInventory = GameObject.FindGameObjectWithTag("Player")?.GetComponentInChildren<Inventory>();
        if (playerInventory != null && GameSession.savedInventory.weapons != null)
        {
            playerInventory.LoadInventory();
        }
    }
}
