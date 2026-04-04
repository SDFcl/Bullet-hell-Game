using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class RoomEnemysController : MonoBehaviour
{
    [Tooltip("List of enemies in the room. This will be populated automatically at runtime.")]
    public List<GameObject> enemies = new List<GameObject>();

    private Collider2D roomArea;

    public System.Action OnRoomCleared;

    private int enemyCount;

    private void Awake()
    {
        if (roomArea == null)
        {
            if (TryGetComponent(out Collider2D collider))
            {
                roomArea = collider;
                return;
            }
            Debug.LogError("Room area collider is not assigned!");
        }
    }

    private void Start()
    {
        FindEnemiesInRoom();
        SetEnemiesActive(false);
    }

    void FindEnemiesInRoom()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(
            roomArea.bounds.center,
            roomArea.bounds.size,
            0f
        );

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                enemies.Add(hit.gameObject);

                Health health = hit.GetComponent<Health>();
                if (health != null)
                {
                    health.OnDead += OnEnemyDead;
                    enemyCount++;
                }
            }
        }
    }

    void OnEnemyDead()
    {
        enemyCount--;

        if (enemyCount <= 0)
        {
            OnRoomCleared?.Invoke();
        }
    }

    public void SetEnemiesActive(bool state)
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(state);
            }
        }
    }
}