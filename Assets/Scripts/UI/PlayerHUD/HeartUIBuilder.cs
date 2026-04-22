using UnityEngine;
using UnityEngine.UI;

public class HeartUIBuilder : MonoBehaviour
{
    private PlayerHealth playerHealth;
    [SerializeField] private Image heartPrefab;
    private float hpPerHeart = 1f;

    void Awake()
    {
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
                playerHealth = player.GetComponent<PlayerHealth>();
        }
    }

    void Start()
    {
        if (playerHealth == null || heartPrefab == null)
            return;

        int heartCount = Mathf.CeilToInt(playerHealth.MaxHealth / hpPerHeart);

        for (int i = 0; i < heartCount; i++)
        {
            Instantiate(heartPrefab, transform);
        }
    }
}
