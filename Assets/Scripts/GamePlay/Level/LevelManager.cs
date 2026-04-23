using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelDataBaseSO levelData;
    private List<GameObject> pool;
    private int lastRandomIndex = -1;
    private LevelID currentLevel;

    public void SpawnLevel()
    {
        if(pool == null || pool.Count == 0)
        {
            Debug.LogWarning("Pool is empty. Make sure to load a level first.");
            return;
        }
        Instantiate(GetRandomPrefab(), Vector3.zero, Quaternion.identity); 
    }

    public void SpawnCurrentLevel()
    {
        if(pool == null || pool.Count == 0)
        {
            Debug.LogWarning("Pool is empty. Make sure to load a level first.");
            return;
        }
        GameObject prefab = GetPrefab();
        if (prefab == null)
        {
            prefab = GetRandomPrefab();
        }
        Instantiate(prefab, Vector3.zero, Quaternion.identity); 
    }
    public void LoadLevel()
    {
        currentLevel = GameSession.currentLevel;
        lastRandomIndex = GameSession.lastRandomIndex;

        LevelDataSO data = levelData.GetLevelData(currentLevel);
        if (data == null)
        {
            Debug.LogWarning($"Level ID {currentLevel} not found in database.");
            return;
        }
        pool = new List<GameObject>(data.levelPrefabs);
    }

    public GameObject GetRandomPrefab()
    {
        if(pool == null || pool.Count == 0)
        {
            Debug.LogWarning("Pool is empty. Make sure to load a level first.");
            return null;
        }

        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, pool.Count);
        }
        while (randomIndex == lastRandomIndex);
        GameSession.lastRandomIndex = randomIndex;

        return pool[randomIndex];
    }

    public GameObject GetPrefab()
    {
        if (pool == null || pool.Count == 0)
        {
            Debug.LogWarning("Pool is empty. Make sure to load a level first.");
            return null;
        }
        if (GameSession.lastRandomIndex < 0 || GameSession.lastRandomIndex >= pool.Count)
        {
            Debug.LogWarning("Invalid lastRandomIndex, cannot get saved prefab.");
            return null;
        }
        return pool[GameSession.lastRandomIndex];
    }

    public int GetMetaCurrencyReward()
    {
        LevelDataSO data = levelData.GetLevelData(GameSession.currentLevel);
        if (data != null)
        {
            GameSession.CurrentReward += data.metaCurrencyReward;
            return data.metaCurrencyReward;
        }
        return 0;
    }

}
