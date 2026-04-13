using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
//using static Pathfinding.Util.GridLookup<T>;

public class EnemySpawner : MonoBehaviour
{
    /*[Header("Enemy List")]
    public List<EnemyData> enemyList;*/

    [Header("Spawn Settings")]
    public LayerMask obstacleLayer;
    public float checkRadius = 1.2f;
    public float spawnRange = 10f;

    [Header("Budget Settings")]
    private int EnemyCount;
    public int MinEnemies = 3;
    public int MaxEnemies = 5;

    [Header("Debug")]
    public bool spawnOnStart = true;

    public Collider roomCollider;

    void Start()
    {
        if (spawnOnStart)
            SpawnEnemies();
    }

    // =========================
    // 🎯 MAIN
    // =========================
    public void SpawnEnemies()
    {
        // int budget = CalculateBudget();
        // List<EnemyData> toSpawn = GenerateByBudget(budget);

        // foreach (var enemy in toSpawn)
        // {
        //     Vector3 pos = FindValidSpawnPosition();

        //     Instantiate(enemy.prefab, pos, Quaternion.identity);
        // }
    }

    // =========================
    // 🧠 Budget System
    // =========================
    int CalculateBudget()
    {
        if (EnemyCount > 0)
            return EnemyCount;
        EnemyCount = Random.Range(MinEnemies, MaxEnemies + 1);
        
        return EnemyCount;
    }

    /*List<EnemyData> GenerateByBudget(int budget)
    {
        List<EnemyData> result = new List<EnemyData>();

        int safety = 50;

        while (budget > 0 && safety-- > 0)
        {
            EnemyData enemy = GetRandomEnemy();

            if (enemy.cost <= budget)
            {
                result.Add(enemy);
                budget -= enemy.cost;
            }
            else
            {
                break;
            }
        }

        return result;
    }*/

   /* EnemyData GetRandomEnemy()
    {
        float total = 0;

        foreach (var e in enemyList)
            total += e.spawnWeight;

        float rand = Random.Range(0, total);

        float current = 0;

        foreach (var e in enemyList)
        {
            current += e.spawnWeight;

            if (rand <= current)
                return e;
        }

        return enemyList[0];
    }*/

    // =========================
    // 📍 Spawn Position (A*)
    // =========================
    Vector3 FindValidSpawnPosition()
    {
        Vector3 center = GetCenter();

        for (int i = 0; i < 20; i++)
        {
            Vector3 random = center + new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0,
                Random.Range(-spawnRange, spawnRange)
            );

            var node = AstarPath.active.GetNearest(random);

            if (node.node != null && node.node.Walkable)
            {
                Vector3 pos = (Vector3)node.position;

                if (IsValidPosition(pos))
                    return pos;
            }
        }

        return center;
    }

    // =========================
    // 🚫 Check กันชน
    // =========================
    bool IsValidPosition(Vector3 pos)
    {
        return !Physics.CheckSphere(pos, checkRadius, obstacleLayer);
    }

    

    public Vector3 GetCenter()
    {
        return roomCollider.bounds.center;
    }

    public float GetSize()
    {
        Vector3 size = roomCollider.bounds.size;
        return size.x * size.z;
    }
}