using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour, ILevel, IDataPersistence
{
    [SerializeField] private LevelID levelToEnter;
    [SerializeField] private string sceneName;

    public void Execute()
    {
        if (!GameSession.isGamePlaying)
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
            GameSession.isGamePlaying = false;
            SceneLoader.Instance.LoadScene("GameClear", gameObject);
        }
    }
    public void LoadData(GameData data)
    {
        GameSession.isGamePlaying = data.OnGamePlaying;
        GameSession.timeCount = data.SavedTimeCount;
        GameSession.enemyCount = data.SavedenemyCount;
        GameSession.CurrentReward = data.CurrentReward;
    }

    public void SaveData(ref GameData data)
    {

    }
}