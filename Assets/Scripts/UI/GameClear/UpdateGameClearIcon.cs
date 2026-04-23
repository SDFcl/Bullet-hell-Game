using UnityEngine;

public class UpdateGameClearIcon : MonoBehaviour, IDataPersistence
{
    public GameObject gameclearIcon;
    public GameObject gameOverIcon;
    void Start()
    {
        if(GameSession.savedHealth == 0)
        {
            gameOverIcon.SetActive(true);
        }
        else
        {
            gameclearIcon.SetActive(true);
        }
        GameSession.isGamePlaying = false;
        DataPersistenceManager dataPersistenceManager = FindObjectOfType<DataPersistenceManager>();
        if (dataPersistenceManager != null)
        {
            dataPersistenceManager.SaveGame();
        }
    }

    public void LoadData(GameData data)
    {

    }
    public void SaveData(ref GameData data)
    {
        data.OnGamePlaying = GameSession.isGamePlaying;
    }
}
