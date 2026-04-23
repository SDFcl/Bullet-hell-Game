using UnityEngine;

public class EnemyKillReporter : MonoBehaviour
{
    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        if (EnemyKillCounter.Instance != null)
        {
            EnemyKillCounter.Instance.RegisterEnemy(health);
        }
    }

    private void OnDisable()
    {
        if (EnemyKillCounter.Instance != null)
        {
            EnemyKillCounter.Instance.UnregisterEnemy(health);
        }
    }
}
