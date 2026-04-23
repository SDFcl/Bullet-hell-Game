using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour, ILevel, IDataPersistence
{
    [SerializeField] private LevelID levelToEnter;
    [SerializeField] private string sceneName;

    public void Execute()
    {
        GameSession.currentLevel = levelToEnter;
        GameSession.lastRandomIndex = -1;
        GameSession.savedInventory = new SavedInventory();
        GameSession.isGamePlaying = true;
        DataPersistenceManager dataPersistenceManager = FindObjectOfType<DataPersistenceManager>();
        if (dataPersistenceManager != null)
        {
            dataPersistenceManager.SaveGame();
            Debug.Log("Game saved before loading level: " + sceneName);
        }
        SceneLoader.Instance.LoadScene(sceneName, gameObject);
    }

    public void setLevelMap(int map)
    {
        levelToEnter.map = map;
    }
    public int getLevelMap()
    {
        return levelToEnter.map;
    }

    public void TryExecute()
    {
        if (GameSession.isGamePlaying)
        {
            SceneLoader.Instance.LoadScene(sceneName, gameObject);
        }
    }
    public void LoadData(GameData data)
    {
        LevelID level = new LevelID(
            data.currentMap,
            (Stage)data.currentStage
        );

        GameSession.currentLevel = level;
        GameSession.isGamePlaying = data.OnGamePlaying;
    }

    public void SaveData(ref GameData data)
    {
        LevelID level = GameSession.currentLevel;

        Debug.Log($"Saving OnGamePlaying: {GameSession.isGamePlaying}");
        data.OnGamePlaying = GameSession.isGamePlaying;
        data.currentMap = level.map;
        data.currentStage = (int)level.stage;
    }
}