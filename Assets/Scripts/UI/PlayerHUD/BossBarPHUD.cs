using UnityEngine;
using UnityEngine.UI;

public class BossBarPHUD : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject bossBarRoot;
    [SerializeField] private Slider bossHpSlider;

    [Header("Detection")]
    [SerializeField] private float detectRange = 12f;
    [SerializeField] private Transform player;
    [SerializeField] private Health bossHealth;

    private KillAllEnemy[] bossMarkers;
    private Health currentBossHealth;

    private void Awake()
    {
        if (bossBarRoot == null)
        {
            bossBarRoot = gameObject;
        }

        if (bossHpSlider == null)
        {
            bossHpSlider = GetComponentInChildren<Slider>();
        }

        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }

        bossMarkers = FindObjectsOfType<KillAllEnemy>(true);
        HideBossBar();
    }

    private void OnEnable()
    {
        if (bossHealth != null)
        {
            BindBoss(bossHealth);
        }
    }

    private void OnDisable()
    {
        UnbindBoss();
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        if (currentBossHealth != null)
        {
            if (currentBossHealth.IsDead)
            {
                HideBossBar();
                UnbindBoss();
                return;
            }

            float distanceToCurrentBoss = Vector2.Distance(player.position, currentBossHealth.transform.position);
            if (distanceToCurrentBoss > detectRange)
            {
                HideBossBar();
                UnbindBoss();
                return;
            }

            ShowBossBar();
            UpdateBossHealth(currentBossHealth.CurrentHP);
            return;
        }

        Health detectedBoss = FindBossInRange();
        if (detectedBoss != null)
        {
            BindBoss(detectedBoss);
            ShowBossBar();
            UpdateBossHealth(detectedBoss.CurrentHP);
        }
        else
        {
            HideBossBar();
        }
    }

    private Health FindBossInRange()
    {
        if (bossHealth != null)
        {
            if (!bossHealth.IsDead && Vector2.Distance(player.position, bossHealth.transform.position) <= detectRange)
            {
                return bossHealth;
            }

            return null;
        }

        if (bossMarkers == null || bossMarkers.Length == 0)
        {
            bossMarkers = FindObjectsOfType<KillAllEnemy>(true);
        }

        Health nearestBoss = null;
        float nearestDistance = float.MaxValue;

        foreach (KillAllEnemy marker in bossMarkers)
        {
            if (marker == null)
            {
                continue;
            }

            Health health = marker.GetComponent<Health>();
            if (health == null || health.IsDead)
            {
                continue;
            }

            float distance = Vector2.Distance(player.position, marker.transform.position);
            if (distance > detectRange || distance >= nearestDistance)
            {
                continue;
            }

            nearestDistance = distance;
            nearestBoss = health;
        }

        return nearestBoss;
    }

    private void BindBoss(Health newBossHealth)
    {
        if (newBossHealth == null || newBossHealth == currentBossHealth)
        {
            return;
        }

        UnbindBoss();
        currentBossHealth = newBossHealth;
        currentBossHealth.OnHealthChanged += UpdateBossHealth;
        currentBossHealth.OnDead += HandleBossDead;

        if (bossHpSlider != null)
        {
            bossHpSlider.minValue = 0f;
            bossHpSlider.maxValue = 1f;
        }
    }

    private void UnbindBoss()
    {
        if (currentBossHealth == null)
        {
            return;
        }

        currentBossHealth.OnHealthChanged -= UpdateBossHealth;
        currentBossHealth.OnDead -= HandleBossDead;
        currentBossHealth = null;
    }

    private void UpdateBossHealth(float currentHp)
    {
        if (bossHpSlider == null || currentBossHealth == null)
        {
            return;
        }

        if (currentBossHealth.MaxHealth <= 0f)
        {
            bossHpSlider.value = 0f;
            return;
        }

        bossHpSlider.value = Mathf.Clamp01(currentHp / currentBossHealth.MaxHealth);
    }

    private void HandleBossDead()
    {
        HideBossBar();
        UnbindBoss();
    }

    private void ShowBossBar()
    {
        if (bossBarRoot != null && !bossBarRoot.activeSelf)
        {
            bossBarRoot.SetActive(true);
        }
    }

    private void HideBossBar()
    {
        if (bossBarRoot != null && bossBarRoot.activeSelf)
        {
            bossBarRoot.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (player == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, detectRange);
    }
}
