using UnityEngine;

public class KillAllEnemy : MonoBehaviour
{
    private Health bossHealh;

    private void Start()
    {
        bossHealh = GetComponent<Health>();
        if (bossHealh != null)
        {
            bossHealh.OnDead += HandleBossDeath;
        }
    }

    private void OnDestroy()
    {
        if (bossHealh != null)
        {
            bossHealh.OnDead -= HandleBossDeath;
        }
    }

    private void HandleBossDeath()
    {
        // เมื่อบอสตายให้หยุดเพลง BGM ทันที
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.StopBGM();
        }

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