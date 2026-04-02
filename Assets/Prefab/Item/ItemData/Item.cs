using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;

    public Item(ItemData data)
    {
        itemData = data;
    }

    public void Use(GameObject user)
    {
        foreach (var effect in itemData.effects)
        {
            effect.Apply(user);
        }
    }
}
