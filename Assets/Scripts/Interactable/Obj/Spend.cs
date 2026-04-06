using UnityEngine;

public enum SpendType
{
    GuideCoines,
    Coins
}

public class Spend : MonoBehaviour
{
    public SpendType spendType;
    public int value = 1;

    public bool SpendOfType(GameObject player)
    {
        /*if (spendType == SpendType.GuideCoines)
        {
            Inventory inventory = player.GetComponent<Inventory>();
            if (inventory != null)
            {
                inventory.AddGuideCoins(1);
            }
            return;
        }*/
        if (spendType == SpendType.Coins)
        {
            Inventory inventory = player.GetComponent<Inventory>();
            if (inventory != null && inventory.SpendCoins(value))
            {
                
            }
            return true;
        }
        return false;
    }
}
