using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : Singleton<DataPersistenceManager>
{
    private GameData gameData;
    private FileDataHandler dataHandler;
    private List<IDataPersistence> dataPersistenceObjects;

    [SerializeField] private string fileName = "save.json";
    private string selectedProfileId = "player1";

    private void Awake()
    {
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 🔥 จุดสำคัญ: เรียกตอน scene โหลดเสร็จ
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        LoadGame();
    }


    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        return FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>()
            .ToList();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    // ======================
    // LOAD
    // ======================
    public void LoadGame()
    {
        if (dataHandler == null)
        {
            Debug.LogError("dataHandler is NULL");
            return;
        }
        this.gameData = dataHandler.Load(selectedProfileId);

        if (this.gameData == null)
        {
            NewGame();
        }

        if (gameData == null)
        {
            Debug.Log("🆕 New Game");
            gameData = new GameData();
        }

        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            obj.LoadData(gameData);
        }
    }

    // ======================
    // SAVE
    // ======================
    public void SaveGame()
    {
        if (gameData == null) return;

        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        foreach (IDataPersistence obj in dataPersistenceObjects)
        {
            Debug.Log("Saving data for: " + obj.GetType().Name);
            obj.SaveData(ref gameData);
        }

        dataHandler.Save(selectedProfileId, gameData);
    }
}