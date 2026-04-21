using TMPro;
using UnityEngine;

public class UIGuildCoin : MonoBehaviour
{
    public TextMeshProUGUI TextGuildCoin;

    private MetaCurrency metaCurrency;

    private void Start()
    {
        MetaCurrency.Instance.OnMetaCurrencyChanged += UpdateGuildCoinDisplay;
    }

    public void UpdateGuildCoinDisplay(int newAmount)
    {
        TextGuildCoin.text = $"{newAmount}";
    }
}
