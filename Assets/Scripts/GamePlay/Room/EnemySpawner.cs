using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
//using static Pathfinding.Util.GridLookup<T>;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public LayerMask obstacleLayer;
    public float checkRadius = 1.2f;
    public float spawnRange = 10f;
    public StartRandomItem startRandomItem;

    [Header("Budget Settings")]
    private int EnemyCount;
    public int MinEnemies = 3;
    public int MaxEnemies = 5;

    [Header("Debug")]
    public bool spawnOnStart = true;

    public Collider2D roomCollider;


    // =========================
    // 🎯 MAIN
    // =========================
    public void SpawnEnemies(Collider2D collider2D,Transform TargetIns = null)
    {
        TargetIns ??= this.transform; // If TargetIns is null, use the spawner's own transform as the target
        roomCollider = collider2D;
        int enemyCount = CalculateBudget();
        Debug.Log($"[EnemySpawner] Enemy count: {enemyCount}");

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemyPrefab = GetRandomEnemyPrefab();
            if (enemyPrefab == null)
                continue;

            Vector3 spawnPos = FindValidSpawnPosition();
            Debug.Log($"[EnemySpawner] Spawning enemy at: {spawnPos}");
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity, TargetIns);
        }
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

    GameObject GetRandomEnemyPrefab()
    {
        if (startRandomItem == null)
        {
            Debug.LogWarning("[EnemySpawner] startRandomItem is not assigned.");
            return null;
        }

        GameObject enemyPrefab = startRandomItem.GetRandomItem();
        if (enemyPrefab == null)
        {
            Debug.LogWarning("[EnemySpawner] StartRandomItem returned null enemy prefab.");
        }

        return enemyPrefab;
    }

    // =========================
    // 📍 Spawn Position (A*)
    // =========================
    Vector3 FindValidSpawnPosition()
    {
        Vector3 center = GetCenter();
        Bounds roomBounds = roomCollider.bounds;

        for (int i = 0; i < 50; i++)
        {
            Vector3 random = new Vector3(
            Random.Range(roomBounds.min.x, roomBounds.max.x),
            Random.Range(roomBounds.min.y, roomBounds.max.y),
             0f
            );

            if (AstarPath.active != null)
            {
                var node = AstarPath.active.GetNearest(random);
                if (node.node != null && node.node.Walkable)
                {
                    Vector3 pos = (Vector3)node.position;
                    if (IsValidPosition(pos))
                        return pos;
                }
            }
            else if (IsValidPosition(random))
            {
                return random;
            }
        }

        if (IsValidPosition(center))
            return center;

        return center + Vector3.up * (checkRadius + 0.1f);
    }

    // =========================
    // 🚫 Check กันชน
    // =========================
    bool IsValidPosition(Vector3 pos)
    {
        return !Physics2D.OverlapCircle(pos, checkRadius, obstacleLayer);
    }

    

    public Vector3 GetCenter()
    {
        return roomCollider.bounds.center;
    }

    public float GetSize()
    {
        Vector3 size = roomCollider.bounds.size;
        return size.x * size.y;
    }
}