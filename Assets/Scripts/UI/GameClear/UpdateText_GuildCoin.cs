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
    }
}
