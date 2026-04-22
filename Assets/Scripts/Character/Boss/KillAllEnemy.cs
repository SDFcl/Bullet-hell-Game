using UnityEngine;

public class KillAllEnemy : MonoBehaviour
{
    private Health bossHealh;

    private void Start()
    {
        bossHealh = GetComponent<Health>();
        bossHealh.OnDead += HandleBossDeath;
    }

    private void HandleBossDeath()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in enemies)
        {
            Debug.Log($"Killing enemy: {enemy.gameObject.name}");
            Health enemyHealth = enemy.GetComponent<Health>();
            if (enemyHealth != null && !enemyHealth.IsDead)
            {
                enemyHealth.Kill();
            }
        }
    }
}
