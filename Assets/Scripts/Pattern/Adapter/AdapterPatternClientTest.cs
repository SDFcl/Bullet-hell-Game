using UnityEngine;

public class AdapterPatternClientTest : MonoBehaviour
{
    private AstarPath pathfinder;
    private LevelManager levelManager;
    private CursorManager cursor;

    private void Awake()
    {
        levelManager = GetComponentInChildren<LevelManager>();
        pathfinder = GetComponentInChildren<AstarPath>();
    }

    private void Start()
    {
        GameLoad();
        GameStart();
    }

    public void GameLoad()
    {
        GameStateManager.Instance.ChangeState(GameState.GamePlay);
        levelManager?.LoadLevel();
    }

    public void GameStart()
    {
        levelManager?.SpawnLevel();
        ConfigureGridFromTilemap();   // ← เพิ่มแค่บรรทัดนี้
        pathfinder?.Scan();

        Inventory playerInventory = GameObject.FindGameObjectWithTag("Player")?.GetComponentInChildren<Inventory>();
        if (playerInventory != null && GameSession.savedInventory.weapons != null)
            playerInventory.LoadInventory();
    }

    private void ConfigureGridFromTilemap()
    {
        if (pathfinder == null) return;

        IGridScanSource scanSource = FindAnyObjectByType<TilemapGridAdapter>();
        if (scanSource == null) return;

        var graph = pathfinder.data.gridGraph;
        Bounds bounds = scanSource.GetBounds();
        float cellSize = scanSource.GetCellSize();

        graph.center = bounds.center;
        graph.SetDimensions(
            Mathf.RoundToInt(bounds.size.x / cellSize),
            Mathf.RoundToInt(bounds.size.y / cellSize),
            cellSize
        );
    }

    public void LoadData(GameData data) { }

    public void SaveData(ref GameData data)
    {
        data.OnGamePlaying = GameSession.isGamePlaying;
        TimeCountGamePlay timeCountGamePlay = FindObjectOfType<TimeCountGamePlay>();
        if (timeCountGamePlay != null)
            data.SavedTimeCount = timeCountGamePlay.timer;
        data.SavedenemyCount = GameSession.enemyCount;
        data.CurrentReward = GameSession.CurrentReward;
    }
}
