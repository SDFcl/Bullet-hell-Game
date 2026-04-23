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
        levelManager?.LoadLevel();
    }
    public void GameStart()
    {
        if (GameSession.isGamePlaying)
        {
            levelManager?.SpawnCurrentLevel();
        }
        else
        {
            levelManager?.SpawnLevel();
        }
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
        data.SavedTimeCount = GameSession.timeCount;
        data.SavedenemyCount = GameSession.enemyCount;
        data.CurrentReward = GameSession.CurrentReward;
    }
}
