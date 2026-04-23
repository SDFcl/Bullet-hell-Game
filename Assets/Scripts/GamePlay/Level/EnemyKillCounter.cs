using System.Collections.Generic;
using UnityEngine;

public class EnemyKillCounter : Singleton<EnemyKillCounter>
{
    protected override bool UseDontDestroyOnLoad => false;

    private readonly HashSet<Health> registeredEnemies = new HashSet<Health>();
    private int enemyKillCount;

    private void Start()
    {
        enemyKillCount = 0;
        GameSession.enemyCout = 0;
    }

    public void RegisterEnemy(Health enemyHealth)
    {
        if (enemyHealth == null) return;
        if (registeredEnemies.Contains(enemyHealth)) return;

        registeredEnemies.Add(enemyHealth);
        enemyHealth.OnDead += CountEnemyKill;
    }

    public void UnregisterEnemy(Health enemyHealth)
    {
        if (enemyHealth == null) return;
        if (!registeredEnemies.Contains(enemyHealth)) return;

        enemyHealth.OnDead -= CountEnemyKill;
        registeredEnemies.Remove(enemyHealth);
    }

    private void CountEnemyKill()
    {
        enemyKillCount++;
        GameSession.enemyCout = enemyKillCount;
    }
}
