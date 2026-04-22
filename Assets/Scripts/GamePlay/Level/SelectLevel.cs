using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour, ILevel
{
    [SerializeField] private LevelID levelToEnter;
    [SerializeField] private string sceneName;

    public void Execute()
    {
        GameSession.currentLevel = levelToEnter;
        GameSession.lastRandomIndex = -1;
        GameSession.savedInventory = new SavedInventory();
        DataPersistenceManager dataPersistenceManager = FindObjectOfType<DataPersistenceManager>();
        if (dataPersistenceManager != null)
        {
            dataPersistenceManager.SaveGame();
            Debug.Log("Game saved before loading level: " + sceneName);
        }
        SceneManager.LoadScene(sceneName);
    }

    public void setLevelMap(int map)
    {
        levelToEnter.map = map;
    }
    public int getLevelMap()
    {
        return levelToEnter.map;
    }
}