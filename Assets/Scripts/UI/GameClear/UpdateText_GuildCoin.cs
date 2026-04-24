using TMPro;
using UnityEngine;

public class UpdateText_GuildCoin : MonoBehaviour
{
    TextMeshProUGUI guildCoinText;
    void Awake()
    {
        guildCoinText = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        guildCoinText.text = GameSession.CurrentReward.ToString();
        MetaCurrency metaCurrency = FindObjectOfType<MetaCurrency>();
        if (metaCurrency != null)
        {
            metaCurrency.AddMetaCurrency(GameSession.CurrentReward);
            Debug.Log($"Added {GameSession.CurrentReward} Guild Coins to MetaCurrency. Total now: {metaCurrency.MetaCurrencyAmount}");
        }
    }
}
