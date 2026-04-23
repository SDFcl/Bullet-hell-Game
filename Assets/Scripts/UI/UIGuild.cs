using TMPro;
using UnityEngine;

public class UIGuildCoin : MonoBehaviour
{
    public TextMeshProUGUI TextGuildCoin;

    private void Start()
    {
        MetaCurrency.Instance.OnMetaCurrencyChanged += UpdateGuildCoinDisplay;
    }

    public void UpdateGuildCoinDisplay(int newAmount)
    {
        TextGuildCoin.text = MetaCurrency.Instance.MetaCurrencyAmount.ToString();
        Debug.Log($"Guild Coin display updated: {newAmount}");
    }
}
