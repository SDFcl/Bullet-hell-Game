using UnityEngine;
using UnityEngine.UI;

public class HeartUIBuilder : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private float currentHeartCount = 0;
    [SerializeField] private Image heartPrefab;
    private float hpPerHeart = 1f;

    void Awake()
    {
        
    }

    void Start()
    {
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                playerHealth = player.GetComponent<PlayerHealth>();
                currentHeartCount = playerHealth.MaxHealth;
            }

           /* PlayerUpgradeManager playerUpgradeManager = FindObjectOfType<PlayerUpgradeManager>();
            if (playerUpgradeManager != null)
            {
                IPlayerStats Stats = playerUpgradeManager.GetFinalStats();
                if (Stats != null)
                {
                    currentHeartCount = playerHealth.MaxHealth;
                    Debug.Log("HeartUIBuilder: Updated PlayerHealth MaxHealth with upgrade stats. New MaxHealth: " + playerHealth.MaxHealth);
                }
            }*/
        }

        if (playerHealth == null || heartPrefab == null)
            return;

        int heartCount = Mathf.CeilToInt(currentHeartCount / hpPerHeart);
        Debug.Log("HeartUIBuilder: Calculated heart count: " + heartCount + " (MaxHealth: " + playerHealth.MaxHealth + ", hpPerHeart: " + hpPerHeart + ")");

        for (int i = 0; i < heartCount; i++)
        {
            Instantiate(heartPrefab, transform);
            Debug.Log("HeartUIBuilder: Instantiated heart prefab for heart " + (i + 1) + "/" + heartCount);
        }
    }
}
