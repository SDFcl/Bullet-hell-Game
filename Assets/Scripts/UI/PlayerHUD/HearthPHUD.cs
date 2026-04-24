using UnityEngine;
using UnityEngine.UI;

public class HearthPHUD : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    private float hpPerHeart = 1f;

    private Image[] hearts;

    void Awake()
    {
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
                playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void OnEnable()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged += UpdateHearts;
    }

    void Start()
    {
        hearts = GetComponentsInChildren<Image>();
        OnEnable(); // Ensure we subscribe to the event before updating the hearts
        UpdateHearts(playerHealth.CurrentHP);
    }

    void OnDisable()
    {
        if (playerHealth != null)
            playerHealth.OnHealthChanged -= UpdateHearts;
    }

    void UpdateHearts(float currentHP)
    {
        if (hearts == null)
            return;

        int fullHeartCount = Mathf.CeilToInt(currentHP / hpPerHeart);

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = i < fullHeartCount ? fullHeart : emptyHeart;
        }
    }
}
