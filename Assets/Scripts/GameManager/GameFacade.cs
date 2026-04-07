using UnityEngine;

public class GameFacade : MonoBehaviour
{
    private AstarPath pathfinder;
    private LevelManager levelManager;
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
        levelManager.LoadLevel();
    }
    public void GameStart()
    {
        levelManager.SpawnLevel();
        pathfinder.Scan();
    }
}
