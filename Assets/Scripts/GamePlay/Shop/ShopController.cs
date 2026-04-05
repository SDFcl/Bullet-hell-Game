using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private RandomItem randomItem;

    public List<Transform> ItemSlot = new List<Transform>();

    private void Awake()
    {
        randomItem = GetComponent<RandomItem>();

        foreach (Transform slot in ItemSlot)
        {
            GameObject item = randomItem.GetRandomItem();
            Debug.Log("Spawning item: " + item.gameObject.name + " in slot: " + slot.name);
            Instantiate(item, slot.position, Quaternion.identity, slot);
        }
    }
}
