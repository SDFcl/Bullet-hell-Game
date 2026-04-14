using UnityEngine;

public class Coins : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory inventory = collision.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddCoins(coinValue);
                gameObject.SetActive(false);
            }
        }
    }
}
