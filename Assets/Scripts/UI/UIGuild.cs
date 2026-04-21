using TMPro;
using UnityEngine;

public class UIGuildCoin : MonoBehaviour
{
    public TextMeshProUGUI TextGuildCoin;

    private MetaCurrency metaCurrency;

    private void Start()
    {
        metaCurrency = MetaCurrency.Instance;
        if (metaCurrency == null)
        {
            Debug.LogError("UIGuildCoin: MetaCurrency instance not found.");
            return;
        }
        metaCurrency.OnMetaCurrencyChanged += UpdateGuildCoinDisplay;
    }

    public void UpdateGuildCoinDisplay(int newAmount)
    {
        TextGuildCoin.text = $"{newAmount}";
    }
}
