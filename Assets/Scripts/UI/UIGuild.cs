using TMPro;
using UnityEngine;

public class UIGuildCoin : MonoBehaviour
{
    public TextMeshProUGUI TextGuildCoin;

    private void OnEnable()
    {
        if (MetaCurrency.Instance != null)
        {
            MetaCurrency.Instance.OnMetaCurrencyChanged += UpdateGuildCoinDisplay;
            UpdateGuildCoinDisplay(MetaCurrency.Instance.MetaCurrencyAmount);
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
        TextGuildCoin.text = MetaCurrency.Instance.MetaCurrencyAmount.ToString();
        Debug.Log($"Guild Coin display updated: {newAmount}");
    }
}
