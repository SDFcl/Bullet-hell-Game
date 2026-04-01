using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;

    public Item(ItemData data)
    {
        itemData = data;
    }
}
