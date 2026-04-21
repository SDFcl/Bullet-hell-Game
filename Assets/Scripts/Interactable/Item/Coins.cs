using UnityEngine;

public class Coins : MonoBehaviour
{
    public int minValue = 1;
    public int maxValue = 10;
    public float multiplier = 1f;

    private IPlayerStats Stats;
    private void OnEnable()
    {
        PlayerUpgradeManager playerUpgradeManager = FindObjectOfType<PlayerUpgradeManager>();
        if (playerUpgradeManager != null)
        {
            Stats = playerUpgradeManager.GetFinalStats();
            multiplier += Stats.BonusCoin / 100;
            //Debug.Log($"Coins: Multiplier updated to {multiplier} based on player stats (BonusCoin: {Stats.BonusCoin}%)");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory inventory = collision.GetComponent<Inventory>();
            if (inventory != null)
            {
                int coinValue = Random.Range(minValue, maxValue + 1);
                coinValue = Mathf.RoundToInt(coinValue * multiplier);
                //Debug.Log($"Coins: Player collected coins worth {coinValue} (base value: {coinValue / multiplier}, multiplier: {multiplier})");
                inventory.AddCoins(coinValue);
                gameObject.SetActive(false);
            }
        }
    }
}
