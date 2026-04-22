using UnityEngine;
using TMPro;

public class CoinTextPHUD : MonoBehaviour
{
    private Inventory playerInventory;
    private TextMeshProUGUI coinText;

    void Awake()
    {
        if (playerInventory == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
                playerInventory = player.GetComponent<Inventory>();
        }
        coinText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        coinText.text = playerInventory.CurrentCoin.ToString();
    }
}
