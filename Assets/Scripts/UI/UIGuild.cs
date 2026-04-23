using TMPro;
using UnityEngine;

public class UIGuildCoin : MonoBehaviour
{
    public TextMeshProUGUI TextGuildCoin;
    MetaCurrency metaCurrency;

    private void Awake()
    {
        metaCurrency = FindObjectOfType<MetaCurrency>();
        if (metaCurrency != null)
        {
            metaCurrency.OnMetaCurrencyChanged += UpdateGuildCoinDisplay;
            UpdateGuildCoinDisplay(metaCurrency.MetaCurrencyAmount);
        }
    }

    private void OnDisable()
    {
        if (MetaCurrency.Instance != null)
        {
            MetaCurrency.Instance.OnMetaCurrencyChanged -= UpdateGuildCoinDisplay;
        }
    }

    public void UpdateGuildCoinDisplay(int newAmount)
    {
        TextGuildCoin.text = metaCurrency.MetaCurrencyAmount.ToString();
        Debug.Log($"Guild Coin display updated: {newAmount}");
    }
}
