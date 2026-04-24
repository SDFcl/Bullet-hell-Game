using UnityEngine;

public class GameFacade : MonoBehaviour,IDataPersistence
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
        pathfinder?.Scan();

        // Load player inventory after spawning level
        Inventory playerInventory = GameObject.FindGameObjectWithTag("Player")?.GetComponentInChildren<Inventory>();
        if (playerInventory != null && GameSession.savedInventory.weapons != null)
        {
            playerInventory.LoadInventory();
        }
    }

    public void LoadData(GameData data)
    {

    }

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
