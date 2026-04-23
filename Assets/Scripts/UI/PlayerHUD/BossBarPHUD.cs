using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossBarPHUD : MonoBehaviour
{
    [SerializeField] private GameObject bossBarRoot;
    [SerializeField] private Slider bossHpSlider;
    [SerializeField] private Transform player;
    [SerializeField] private float range = 10f;
    [SerializeField] private LayerMask bossLayer;
    [SerializeField] private float fadeSpeed = 8f;
    [SerializeField] private TextMeshProUGUI nameBoss;

    private Health currentBossHealth;
    private CanvasGroup canvasGroup;
    private float targetAlpha;

    private void Awake()
    {
        if (bossBarRoot == null)
        {
            bossBarRoot = gameObject;
        }

        canvasGroup = bossBarRoot.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = bossBarRoot.AddComponent<CanvasGroup>();
        }

        if (bossHpSlider == null)
        {
            bossHpSlider = GetComponentInChildren<Slider>(true);
        }

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        if (bossHpSlider != null)
        {
            bossHpSlider.minValue = 0f;
            bossHpSlider.maxValue = 1f;
        }

        bossBarRoot.SetActive(true);
        targetAlpha = 0f;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        UpdateFade();

        if (player == null)
            return;

        Collider2D hit = FindBossInRange();

        if (hit == null)
        {
            HideBossBar();
            ClearBoss();
            return;
        }

        if (!hit.CompareTag("Enemy"))
            return;

        Health bossHealth = hit.GetComponent<Health>();
        if (bossHealth == null)
            bossHealth = hit.GetComponentInParent<Health>();

        if (bossHealth == null)
            return;

        if (currentBossHealth != bossHealth)
        {
            SetBoss(bossHealth);
        }

        if (currentBossHealth.IsDead)
        {
            HideBossBar();
            ClearBoss();
            return;
        }

        ShowBossBar();
        UpdateBossBar(currentBossHealth.CurrentHP);
    }

    public void SetBoss(Health bossHealth)
    {
        if (bossHealth == null)
            return;

        ClearBoss();
        

        currentBossHealth = bossHealth;
        currentBossHealth.OnHealthChanged += UpdateBossBar;
        currentBossHealth.OnDead += HandleBossDead;

        nameBoss.text = currentBossHealth.gameObject.GetComponent<BossName>().bossname;
        UpdateBossBar(currentBossHealth.CurrentHP);
    }

    private void ClearBoss()
    {
        if (currentBossHealth == null)
            return;

        currentBossHealth.OnHealthChanged -= UpdateBossBar;
        currentBossHealth.OnDead -= HandleBossDead;
        currentBossHealth = null;
    }

    private void UpdateBossBar(float currentHp)
    {
        if (bossHpSlider == null || currentBossHealth == null)
            return;

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
        ClearBoss();
    }

    private void ShowBossBar()
    {
        targetAlpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void HideBossBar()
    {
        targetAlpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private Collider2D FindBossInRange()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(player.position, range, bossLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                return hit;
            }
        }

        return null;
    }

    private void UpdateFade()
    {
        if (canvasGroup == null)
            return;

        canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, targetAlpha, fadeSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        if (player == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, range);
    }
}
